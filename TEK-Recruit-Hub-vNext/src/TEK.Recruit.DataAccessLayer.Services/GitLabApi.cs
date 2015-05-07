using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using TEK.Recruit.Commons;
using TEK.Recruit.Commons.Entities.GitLab;
using TEK.Recruit.Commons.Extensions;
using TEK.Recruit.Framework.Configuration.Services;
using TEK.Recruit.Framework.Http;
using TEK.Recruit.Framework.Http.Services;

namespace TEK.Recruit.DataAccessLayer.Services
{
    [ExcludeFromCodeCoverage]
    public class GitLabApi : IGitLabApi
    {
        private readonly IProvideConfig _configProvider;
        private readonly IHandleHttpRequest _httpRequestHandler;

        public GitLabApi(IProvideConfig configProvider, IHandleHttpRequest httpRequestHandler)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (httpRequestHandler == null) throw new ArgumentNullException("httpRequestHandler");
            _configProvider = configProvider;
            _httpRequestHandler = httpRequestHandler;
            _httpRequestHandler.Init(configProvider.GetGitLabBaseUrl(), configProvider.GetGitLabApiVersion());
        }

        public async Task<string> GetAdminToken()
        {
            return await GetSecurityToken(_configProvider.GetAdminEmail(), _configProvider.GetAdminPassword());
        }
        public async Task<string> GetSecurityToken(string email, string password)
        {
            var gitLabResponse = await _httpRequestHandler.PostHttpRequest<GitLabSecurityToken>(string.Format("session?email={0}&password={1}", email, password), null, null);
            return gitLabResponse.ReturnedObject.PrivateToken;
        }
        public async Task<bool> RemoveUserAccess(string projectId, string candidateId, string adminToken)
        {
            var url = string.Format("projects/{0}/members/{1}",
                projectId,
                candidateId);

            return await _httpRequestHandler.DeleteHttpRequest(url, adminToken);
        }
        public async Task<RestApiResponse<GitLabUser>> CreateUser(string name, string email, string username, string password, string adminToken)
        {
            var user = new GitLabUser()
            {
                Name = name,
                Email = email,
                Password = password,
                Username = username,
                Confirm = true,
                CanCreateGroup = false,
                IsAdmin = false,
                ProjectsLimit = 1
            };
            return await _httpRequestHandler.PostHttpRequest<GitLabUser>("users", user, adminToken);
        }
        public async Task<RestApiResponse<GitLabProject>> ForkRepository(string email, string password, string devEnvRepoName, string adminToken)
        {
            var sourceProject = await GetProjectTemplate(devEnvRepoName, adminToken);
            var userToken = await GetSecurityToken(email, password);
            return await _httpRequestHandler.PostHttpRequest<GitLabProject>(string.Format("projects/fork/{0}", sourceProject.Id), null, userToken);
        }
        public async Task<GitLabGroup[]> SearchCodingExerciseGroup(string adminToken)
        {
            return await _httpRequestHandler.GetHttpRequest<GitLabGroup[]>(string.Format("groups?search={0}", _configProvider.GetCodingExerciseGroupName()), adminToken);
        }
        public async Task<RestApiResponse<GitLabGroup>> CreateCodingExerciseGroup(string adminToken)
        {
            var groupName = _configProvider.GetCodingExerciseGroupName();
            var group = new GitLabGroup()
            {
                Name = groupName,
                Path = groupName,
                Description = "This is the group to hold all coding exercises"
            };

            return await _httpRequestHandler.PostHttpRequest<GitLabGroup>("groups/", group, adminToken);
        }
        public async Task<RestApiResponse<GitLabGroup>> MoveForkedRepoToGroup(string groupId, string projectId, string adminToken)
        {
            return await _httpRequestHandler.PostHttpRequest<GitLabGroup>(string.Format("groups/{0}/projects/{1}", groupId, projectId), null, adminToken);
        }
        public async Task<GitLabUser[]> GetProjectMembers(string projectId, string adminToken)
        {
            return await _httpRequestHandler.GetHttpRequest<GitLabUser[]>(string.Format("projects/{0}/members", projectId), adminToken);
        }
        public async Task<RestApiResponse<GitLabUser>> AddCandidateToGroupMembers(string groupId, string userId, string adminToken)
        {
            var json = new GitLabMembership()
            {
                Id = groupId,
                UserId = userId,
                AccessLevel = GitLabLevelAccess.DEVELOPER.NumericValue()
            };
            return await _httpRequestHandler.PostHttpRequest<GitLabUser>(string.Format("groups/{0}/members", groupId), json, adminToken);
        }
        public async Task<GitLabProject> GetProjectById(string projectId, string adminToken)
        {
            return await _httpRequestHandler.GetHttpRequest<GitLabProject>(string.Format("projects/{0}", projectId), adminToken);
        }
        public async Task<GitLabCommit[]> GetProjectCommits(string projectId, string adminToken)
        {
            return await _httpRequestHandler.GetHttpRequest<GitLabCommit[]>(string.Format("projects/{0}/repository/commits", projectId), adminToken);
        }
        public async Task<GitLabUser[]> GetUsers(string adminToken)
        {
            return await _httpRequestHandler.GetHttpRequest<GitLabUser[]>("users", adminToken);
        }
        public async Task<GitLabUser> GetUserById(string candidateId, string adminToken)
        {
            return await _httpRequestHandler.GetHttpRequest<GitLabUser>(string.Format("users/{0}", candidateId), adminToken);
        }
        public async Task<GitLabProject[]> GetUserProjects(string userId, string email, string password)
        {
            var userToken = await GetSecurityToken(email, password);
            return await _httpRequestHandler.GetHttpRequest<GitLabProject[]>("projects/owned", userToken);
        }
        public async Task<GitLabProject[]> GetAllProjects(string adminToken)
        {
            return await _httpRequestHandler.GetHttpRequest<GitLabProject[]>("projects/all", adminToken);
        }
        public async Task<bool> DeleteProject(string projectid, string adminToken)
        {
            return await _httpRequestHandler.DeleteHttpRequest("projects/" + projectid, adminToken);
        }
        public async Task<bool> DeleteUser(string candidateid, string adminToken)
        {
            return await _httpRequestHandler.DeleteHttpRequest("users/" + candidateid, adminToken);
        }
        public async Task<bool> DeleteGroup(string groupId, string adminToken)
        {
            return await _httpRequestHandler.DeleteHttpRequest("groups/" + groupId, adminToken);
        }
        public async Task<RestApiResponse<GitLabUser>> SetCandidateAsDevolperOfForkedProject(string projectId, string userId, string adminToken)
        {
            var url = string.Format("projects/{0}/members/{1}?access_level={2}",
                projectId,
                userId,
                GitLabLevelAccess.DEVELOPER.NumericValue());

            return await _httpRequestHandler.PutHttpRequest<GitLabUser>(url, null, adminToken);
        }
        public async Task<RestApiResponse<GitLabUser>> SetAdminAsOwnerOfForkedProject(string projectId, string adminToken)
        {
            var admin = await GetUserByUsername(_configProvider.GetAdminUsername(), adminToken);
            var url = string.Format("projects/{0}/members?user_id={1}&access_level={2}",
                projectId,
                admin.Id,
                GitLabLevelAccess.OWNER.NumericValue());

            return await _httpRequestHandler.PostHttpRequest<GitLabUser>(url, null, adminToken);
        }
        public async Task<RestApiResponse<GitLabProject>> UpdateProjectVisibilityAndName(string projectId, string newName, string adminToken)
        {
            var project = new GitLabProject()
            {
                Id = projectId,
                Name = newName,
                Path = newName,
                VisibilityLevel = GitLabProjectVisibilityLevel.PRIVATE.NumericValue()
            };
            var url = string.Format("projects/{0}", projectId);
            return await _httpRequestHandler.PutHttpRequest<GitLabProject>(url, project, adminToken);
        }
        public async Task<RestApiResponse<GitLabUser>> AddJenkinsUserToForkedProject(string projectId, string adminToken)
        {
            var jenkinsUser = await GetUserByUsername(_configProvider.GetJenkinsUsername(), adminToken);
            var url = string.Format("projects/{0}/members?user_id={1}&access_level={2}",
                projectId,
                jenkinsUser.Id,
                GitLabLevelAccess.DEVELOPER.NumericValue());

            return await _httpRequestHandler.PostHttpRequest<GitLabUser>(url, null, adminToken);
        }

        private async Task<GitLabUser> GetUserByUsername(string username, string adminToken)
        {
            var users = await GetUsers(adminToken);
            return users.ToList().First(u => u.Username == username);
        }
        private async Task<GitLabProject> GetProjectTemplate(string devEnvRepoName, string adminToken)
        {
            var groupeName = _configProvider.GetCodingExerciseGroupName();
            var projects = await _httpRequestHandler.GetHttpRequest<GitLabProject[]>(string.Format("projects/search/{0}?order_by=created_at", devEnvRepoName), adminToken);
            
            var sourceProject = projects.ToList()
                .Where(p => !p.PatWithNamespace.Contains(groupeName))
                .Where(p => p.Owner.Name.ToLower() == _configProvider.GetAdminName())
                .OrderBy(p => p.CreatedAt)
                .First(p => p.Name == devEnvRepoName);
            return sourceProject;
        }
    }
}