using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessServices.Contracts;
using Commons;
using Commons.Dtos;
using Commons.Entities;
using Commons.Entities.ElasticSearch;
using Commons.Entities.Geolocation;
using Commons.Entities.GitLab;
using Commons.Entities.Sonar;
using Commons.Extensions;
using DataAccessLayer.Contracts;
using Framework.Configuration.Contracts;

namespace BusinessServices.Services
{
    public class CodingExcerciseEnvironmentManager : IManageCodingExcerciseEnvironment
    {
        private readonly IGitLabApi _gitLabApi;
        private readonly IElasticSearhApi _elasticSearhApi;
        private readonly ISonarApi _sonarApi;
        private readonly IEmailService _emailService;
        private readonly IProvideConfig _configProvider;
        private readonly IGenerateElasticSearchReports _elasticSearchReportGenerator;
        private readonly IGeolocator _geolocator;
        private readonly IGeoHash _geoHash;

        public CodingExcerciseEnvironmentManager(IGitLabApi gitLabApi, IElasticSearhApi elasticSearhApi, ISonarApi sonarApi, IEmailService emailService, IProvideConfig configProvider, IGenerateElasticSearchReports elasticSearchReportGenerator, IGeolocator geolocator, IGeoHash geoHash)
        {
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            if (elasticSearhApi == null) throw new ArgumentNullException("elasticSearhApi");
            if (sonarApi == null) throw new ArgumentNullException("sonarApi");
            if (emailService == null) throw new ArgumentNullException("emailService");
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (elasticSearchReportGenerator == null) throw new ArgumentNullException("elasticSearchReportGenerator");
            if (geolocator == null) throw new ArgumentNullException("geolocator");
            if (geoHash == null) throw new ArgumentNullException("geoHash");
            _gitLabApi = gitLabApi;
            _elasticSearhApi = elasticSearhApi;
            _sonarApi = sonarApi;
            _emailService = emailService;
            _configProvider = configProvider;
            _elasticSearchReportGenerator = elasticSearchReportGenerator;
            _geolocator = geolocator;
            _geoHash = geoHash;
        }

        public async Task<GitLabProject[]> GetAllCandiatesProjects()
        {
            var adminToken = await GetAdminToken();
            var allProjects = await _gitLabApi.GetAllProjects(adminToken);
            var projects = allProjects.Where(p => p.PatWithNamespace.Contains(_configProvider.GetCodingExerciseGroupName())).ToArray();
            foreach (var project in projects)
            {
                var members = await _gitLabApi.GetProjectMembers(project.Id, adminToken);
                var devMembers = members.Where(m => m.AccessLevel == GitLabLevelAccess.DEVELOPER.NumericValue());
                project.Members = devMembers;
            }
            return projects;
        }

        public async Task<bool> DeleteCandidateTestEnv(string projectId, string candidateId)
        {
            var adminToken = await GetAdminToken();
            var success = await _gitLabApi.DeleteProject(projectId, adminToken);
            if (success)
                success = await _gitLabApi.DeleteUser(candidateId, adminToken);
            return success;
        }

        public async Task<EnvironmentSetUpResult> StartEnvironmentCreation(string name, string email, string username, string devEnv)
        {
            var adminToken = await GetAdminToken();
            var result = await CreateUser(name, email, username, adminToken);
            if (result.Success)
            {
                result = await CreateCodingTestGroupIfNotExists(result, adminToken);
            }
            if (result.Success)
            {
                result = await ForkRepository(result, devEnv, adminToken);
            }
            //if (result.Success)
            //{
            //    result = await AddCandidateToGroupMembers(result, adminToken);
            //}
            if (result.Success)
            {
                result = await MoveForkedRepoToGroup(result, adminToken);
            }
            if (result.Success)
            {
                result = await SetAdminAsOwnerOfForkedProject(result, adminToken);
            }
            if (result.Success
                && result.DevEnv == DevEnv.Java)
            {
                result = await AddJenkinsUserToForkedProject(result, adminToken);
            }
            if (result.Success)
            {
                result = await SetCandidateAsDevolperOfForkedProject(result, adminToken);
            }
            if (result.Success)
            {
                result = await UpdateProjectVisibilityAndName(result, adminToken);
            }
            if (result.Success)
            {
                result = SendEmailNotifications(result);
            }
            if (result.Success)
            {
                result.Message = "Candidate's coding excercise repository is ready";
            }
            return result;
        }

        public async Task<bool> RemoveUserAccess(string projectId, string candidateId)
        {
            var adminToken = await GetAdminToken();
            return await _gitLabApi.RemoveUserAccess(projectId, candidateId, adminToken);
        }

        public async Task<bool> GenerateReport(string projectId, string candidateid)
        {
            var adminToken = await GetAdminToken();

            var firstBatchTasks = new Task[]
            {
                _gitLabApi.GetProjectById(projectId, adminToken),
                _gitLabApi.GetProjectCommits(projectId, adminToken),
                GetCandiateEvalById(projectId, candidateid),
                GetRecuiterReportElasticSearchRecordId(projectId, candidateid)
            };
            await Task.WhenAll(firstBatchTasks);

            //TODO: Check and set Elastic Search Mapping before first report creation
            
            var project = ((Task<GitLabProject>) firstBatchTasks[0]).Result;
            var commits = ((Task<GitLabCommit[]>)firstBatchTasks[1]).Result;
            // TODO: commits should be only candidate's commits - need to get candidate user from gitlab and lookup by author name

            var candidateEval = ((Task<CandidateEvaluation>)firstBatchTasks[2]).Result;
            var elasticSearchRecordId = ((Task<string>)firstBatchTasks[3]).Result;

            var secondBatchTasks = new Task[]
            {
                GetSonarMetrics(project.Name),
                _geolocator
                    .GetGeoCoordonateByCity(candidateEval.City)
                    .ContinueWith( previous => GetGeoHash(previous.Result).Result)
            };
            await Task.WhenAll(secondBatchTasks);
            var sonarMetrics = ((Task<SonarMetrics>)secondBatchTasks[0]).Result;
            var geoHash = ((Task<string>)secondBatchTasks[1]).Result;

            var newRecuiterReport = await _elasticSearchReportGenerator.GenerateRecruiterReport(project, commits, sonarMetrics, candidateEval, geoHash);

            var response = elasticSearchRecordId != null
                ? await _elasticSearhApi.UpdateRecuiterReport(elasticSearchRecordId, newRecuiterReport)
                : await _elasticSearhApi.CreateRecuiterReport(newRecuiterReport);
            return response.SuccessfullyCreated;
        }

        public async Task<bool> SaveCandidateEvaluation(CandidateEvaluationDto dto)
        {
            var elasticSearchCandidateEvaluation = new CandidateEvaluation
            {
                ProjectId = dto.ProjectId,
                CandidateId = dto.CandidateId,
                City = dto.City,
                CodeQuality = dto.CodeQuality,
                Country = dto.Country,
                CulturalFit = dto.CulturalFit,
                Position = dto.Position,
                PostalCode = dto.PostalCode,
                State = dto.State,
                TechnicalInterview = dto.TechnicalInterview,
                TekLocation = dto.TekLocation
            };

            var elasticSearchRecordId = await GetCandidateEvaluationElasticSearchRecordId(dto.ProjectId, dto.CandidateId);
            var response = elasticSearchRecordId != null
                ? await _elasticSearhApi.UpdateCandidateEvaluation(elasticSearchRecordId, elasticSearchCandidateEvaluation)
                : await _elasticSearhApi.CreateCandidateEvaluation(elasticSearchCandidateEvaluation);
            return response.SuccessfullyCreated;
        }

        public async Task<CandidateEvaluation> GetCandidateEvaluation(string projectid, string candidateid)
        {
            return await GetCandiateEvalById(projectid, candidateid);
        }


        private async Task<string> GetAdminToken()
        {
            return await _gitLabApi.GetAdminToken();
        }

        private async Task<EnvironmentSetUpResult> MoveForkedRepoToGroup(EnvironmentSetUpResult environmentSetUpResult, string adminToken)
        {
            var response = await _gitLabApi.MoveForkedRepoToGroup(environmentSetUpResult.Group.Id, environmentSetUpResult.ProjectId, adminToken);
            environmentSetUpResult.Message = response.SuccessfullyCreated ? "Moved forked project into default group" : "An error occured while moving the candidate's project into the default group.";
            environmentSetUpResult.Success = response.SuccessfullyCreated;
            return environmentSetUpResult;
        }

        private async Task<EnvironmentSetUpResult> CreateCodingTestGroupIfNotExists(EnvironmentSetUpResult environmentSetUpResult, string adminToken)
        {
            var search = await _gitLabApi.SearchCodingExerciseGroup(adminToken);
            var groupName = _configProvider.GetCodingExerciseGroupName();
            if (search.ToList().Any(g => g.Name == groupName))
            {
                environmentSetUpResult.Group = search.ToList().First(g => g.Name == groupName);
                environmentSetUpResult.Message = "Default Coding Exercise Group of projects found";
                environmentSetUpResult.Success = true;
            }
            else
            {
                var response = await _gitLabApi.CreateCodingExerciseGroup(adminToken);
                environmentSetUpResult.Group = response.ReturnedObject;
                environmentSetUpResult.Message = response.SuccessfullyCreated ? "Created default Coding Exercise Group" : "Not able to create default Coding Exercise Group of projects";
                environmentSetUpResult.Success = response.SuccessfullyCreated;
            }
            return environmentSetUpResult;
        }

        private async Task<EnvironmentSetUpResult> UpdateProjectVisibilityAndName(EnvironmentSetUpResult environmentSetUpResult, string adminToken)
        {
            var response = await _gitLabApi.UpdateProjectVisibilityAndName(environmentSetUpResult.ProjectId, environmentSetUpResult.User.Username, environmentSetUpResult.DevEnv, adminToken);
            environmentSetUpResult.ProjectId = response.ReturnedObject.Id;
            environmentSetUpResult.HttpUrlToRepo = new Uri(response.ReturnedObject.HttpUrlToRepo);
            environmentSetUpResult.SshUrlToRepo = response.ReturnedObject.SshUrlToRepo;
            environmentSetUpResult.WebUrl = new Uri(response.ReturnedObject.WebUrl);

            environmentSetUpResult.Message = response.SuccessfulRequest ? "Project visibility level set to private, renamed with username concat" : "An error occured while updating the project visibility and name";
            environmentSetUpResult.Success = response.SuccessfulRequest;
            return environmentSetUpResult;
        }

        private async Task<EnvironmentSetUpResult> SetCandidateAsDevolperOfForkedProject(EnvironmentSetUpResult environmentSetUpResult, string adminToken)
        {
            var response = await _gitLabApi.SetCandidateAsDevolperOfForkedProject(environmentSetUpResult.ProjectId, environmentSetUpResult.User.Id, adminToken);
            environmentSetUpResult.Message = response.SuccessfulRequest ? "Candidate set as developer of the forked project" : "An error occured while setting the candidate as developer of the forked project";
            environmentSetUpResult.Success = response.SuccessfulRequest;
            return environmentSetUpResult;
        }

        private async Task<EnvironmentSetUpResult> AddJenkinsUserToForkedProject(EnvironmentSetUpResult environmentSetUpResult, string adminToken)
        {
            var response = await _gitLabApi.AddJenkinsUserToForkedProject(environmentSetUpResult.ProjectId, adminToken);
            environmentSetUpResult.Message = response.SuccessfullyCreated ? "Adminstrator set as owner of the forked project" : "An error occured while setting the Adminstrator as owner of the forked project";
            environmentSetUpResult.Success = response.SuccessfullyCreated;
            return environmentSetUpResult;
        }

        private async Task<EnvironmentSetUpResult> SetAdminAsOwnerOfForkedProject(EnvironmentSetUpResult environmentSetUpResult, string adminToken)
        {
            var response = await _gitLabApi.SetAdminAsOwnerOfForkedProject(environmentSetUpResult.ProjectId, adminToken);
            environmentSetUpResult.Message = response.SuccessfullyCreated ? "Adminstrator set as owner of the forked project" : "An error occured while setting the Adminstrator as owner of the forked project";
            environmentSetUpResult.Success = response.SuccessfullyCreated;
            return environmentSetUpResult;
        }

        private EnvironmentSetUpResult SendEmailNotifications(EnvironmentSetUpResult environmentSetUpResult)
        {
            try
            {
                _emailService.SendEmail(environmentSetUpResult);
                environmentSetUpResult.Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error occured: {0}", ex.Message));
                environmentSetUpResult.Success = false;
                environmentSetUpResult.Message = "An error occured while sending the email to the candidate";
            }
            return environmentSetUpResult;
        }

        private async Task<EnvironmentSetUpResult> ForkRepository(EnvironmentSetUpResult environmentSetUpResult, string devEnv, string adminToken)
        {
            environmentSetUpResult.DevEnv = (DevEnv)Enum.Parse(typeof(DevEnv), devEnv);
            var response = await _gitLabApi.ForkRepository(environmentSetUpResult.User.Email, environmentSetUpResult.User.Password, environmentSetUpResult.DevEnv, adminToken);

            environmentSetUpResult.ProjectId = response.ReturnedObject.Id;

            environmentSetUpResult.Message = response.SuccessfullyCreated ? "Respository forked" : "An error occured while forking the repository";
            environmentSetUpResult.Success = response.SuccessfullyCreated;
            return environmentSetUpResult;
        }

        private async Task<EnvironmentSetUpResult> CreateUser(string name, string email, string username, string adminToken)
        {
            var password = GeneratePassword(username);
            var response = await _gitLabApi.CreateUser(name, email, username, password, adminToken);
            var result = new EnvironmentSetUpResult
            {
                User = response.ReturnedObject,
                Success = response.SuccessfullyCreated,
                Message = response.SuccessfullyCreated ? "User created" : "An error occured while creating the user",
            };
            result.User.Password = password;
            return result;
        }

        private async Task<CandidateEvaluation> GetCandiateEvalById(string projectId, string candidateid)
        {
            var getEvalResponse = await _elasticSearhApi.GetCandidateEvaluation(projectId, candidateid);
            if (getEvalResponse.SuccessfulRequest)
            {
                var candidateEval = !getEvalResponse.ReturnedObject.HitsHeader.Total.Equals(1)
                    ? null
                    : getEvalResponse.ReturnedObject.HitsHeader.Hits.First().Source;
                return candidateEval;
            }
            else
            {
                return null;
            }
        }

        private async Task<SonarMetrics> GetSonarMetrics(string projectName)
        {
            var allSonarMetrics = await _sonarApi.GetAllSonarMetrics();
            var sonarMetrics = allSonarMetrics.FirstOrDefault(sm => sm.Name == projectName);
            return sonarMetrics;
        }

        private async Task<string> GetRecuiterReportElasticSearchRecordId(string projectId, string candidateid)
        {
            string elasticSearchRecordId = null;
            var getReportResponse = await _elasticSearhApi.GetRecuiterReport(projectId, candidateid);
            if (getReportResponse.SuccessfulRequest)
            {
                if (getReportResponse.ReturnedObject != null
                    && getReportResponse.StatusCode != HttpStatusCode.NotFound
                    && getReportResponse.ReturnedObject.HitsHeader.Total.Equals(1))
                {
                    elasticSearchRecordId = getReportResponse.ReturnedObject.HitsHeader.Hits.First().Id;
                }
            }
            return elasticSearchRecordId;
        }

        private async Task<string> GetCandidateEvaluationElasticSearchRecordId(string projectId, string candidateId)
        {
            string elasticSearchRecordId = null;
            var getCandidateEvalResponse = await _elasticSearhApi.GetCandidateEvaluation(projectId, candidateId);
            if (getCandidateEvalResponse.SuccessfulRequest)
            {
                if (getCandidateEvalResponse.ReturnedObject != null
                    && getCandidateEvalResponse.StatusCode != HttpStatusCode.NotFound)
                {
                    if (getCandidateEvalResponse.ReturnedObject.HitsHeader.Total.Equals(1))
                    {
                        elasticSearchRecordId = getCandidateEvalResponse.ReturnedObject.HitsHeader.Hits.First().Id;
                    }
                }
            }
            return elasticSearchRecordId;
        }

        private async Task<string> GetGeoHash(GeoLocation geolocation)
        {
            string location = null;
            if (geolocation != null
                && geolocation.Response != null
                && geolocation.Response.Docs.Any())
            {
                var firstResult = geolocation.Response.Docs.FirstOrDefault();
                if (firstResult != null)
                {
                    location = await _geoHash.GetGeoLocationHash(firstResult.Lat, firstResult.Lng);
                }
            }
            return location;
        }

        private string GeneratePassword(string username)
        {
            var password = username;
            if (username.Length >= 8) return password;

            var rounds = 8 - username.Length;
            var count = 1;
            for (var i = 0; i < rounds; i++)
            {
                password = password + count.ToString(CultureInfo.InvariantCulture);
                count++;
            }
            return password;
        }
    }
}