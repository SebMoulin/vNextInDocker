using System;
using System.Linq;
using System.Threading.Tasks;
using TEK.Recruit.BusinessServices.Services;
using TEK.Recruit.Commons.Dtos;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.Commons.Entities.Interview;

namespace TEK.Recruit.Facade.Services
{
    public class CodingExcerciseEnvironmentFacade : ICodingExcerciseEnvironmentFacade
    {
        private readonly ISetUpCodingExcerciseEnvironment _environmentSetupService;
        private readonly IManageCodingExcerciseEnvironment _codingExcerciseEnvironmentManager;
        private readonly IHandleAdmin _adminService;
        private readonly IHandleCandidateInterview _candidateInterviewService;
        private readonly IHandleReporting _reportingService;
        private readonly IHandleMapping _mappingService;

        public CodingExcerciseEnvironmentFacade(
            ISetUpCodingExcerciseEnvironment environmentSetupService,
            IManageCodingExcerciseEnvironment codingExcerciseEnvironmentManager,
            IHandleAdmin adminService,
            IHandleCandidateInterview candidateInterviewService,
            IHandleReporting reportingService,
            IHandleMapping mappingService
            )
        {
            if (environmentSetupService == null) throw new ArgumentNullException("environmentSetupService");
            if (codingExcerciseEnvironmentManager == null) throw new ArgumentNullException("codingExcerciseEnvironmentManager");
            if (adminService == null) throw new ArgumentNullException("adminService");
            if (candidateInterviewService == null) throw new ArgumentNullException("candidateInterviewService");
            if (reportingService == null) throw new ArgumentNullException("reportingService");
            if (mappingService == null) throw new ArgumentNullException("mappingService");
            _environmentSetupService = environmentSetupService;
            _codingExcerciseEnvironmentManager = codingExcerciseEnvironmentManager;
            _adminService = adminService;
            _candidateInterviewService = candidateInterviewService;
            _reportingService = reportingService;
            _mappingService = mappingService;
        }

        public async Task<EnvironmentSetUpResultDto> CreateCodingExcerciseEnvironment(CandidateEnvironmentDto environmentSetUpDto)
        {
            var result = await _environmentSetupService.CreateCodingExcerciseEnvironment(environmentSetUpDto.CandidateName,
                environmentSetUpDto.Email,
                environmentSetUpDto.Username,
                environmentSetUpDto.CustomerId,
                environmentSetUpDto.CustomerName,
                environmentSetUpDto.DevEnv,
                environmentSetUpDto.City,
                environmentSetUpDto.PostalCode,
                environmentSetUpDto.State,
                environmentSetUpDto.Country,
                environmentSetUpDto.Position,
                environmentSetUpDto.TekCenter,
                environmentSetUpDto.RecruiterEmail);

            return _mappingService.Map<EnvironmentSetUpResult, EnvironmentSetUpResultDto>(result);
        }

        public async Task<CandidateEnvironmentDto[]> GetAllCandidateEnvironments()
        {
            var result = await _codingExcerciseEnvironmentManager.GetAllCandidateEnvironments();
            return _mappingService.Map<CandidateEnvironment[], CandidateEnvironmentDto[]>(result);
        }

        public async Task<bool> DeleteTestEnv(string projectid, string candidateid)
        {
            return await _codingExcerciseEnvironmentManager.DeleteCandidateProject(projectid, candidateid);
        }

        public async Task<bool> RemoveUserAccess(string projectid, string candidateid)
        {
            return await _codingExcerciseEnvironmentManager.RemoveUserAccess(projectid, candidateid);
        }

        public async Task<bool> GenerateReport(string customerId, string projectid, string candidateid)
        {
            return await _reportingService.GenerateReport(customerId, projectid, candidateid);
        }

        public async Task<bool> SaveCandidateInterview(InterviewDto interviewDto)
        {
            var interview = _mappingService.Map<InterviewDto, Interview>(interviewDto);
            return await _candidateInterviewService.SaveCandidateInterview(interview);
        }

        public async Task<InterviewDto> GetCandidateInterview(string projectid, string candidateid)
        {
            var result = await _candidateInterviewService.GetCandidateInterviewById(projectid, candidateid);
            return _mappingService.Map<Interview, InterviewDto>(result);
        }

        public async Task<bool> DeleteRecruiterReportIndex(string customerId)
        {
            return await _adminService.DeleteRecruiterReportIndex(customerId);
        }

        public async Task<bool> CreateRecruiterReportMapping(string customerId)
        {
            return await _adminService.CreateRecruiterReportMapping(customerId);
        }

        public async Task<bool> CreateCustomer(string customerName)
        {
            return await _adminService.CreateCustomer(customerName);
        }

        public async Task<CustomerDto[]> GetAllCustomers()
        {
            var result = await _adminService.GetAllCustomers();
            return _mappingService.Map<Customer[], CustomerDto[]>(result);
        }

        public async Task<bool> CreateDefaultCustomer()
        {
            var allCustomer = await GetAllCustomers();
            if (!allCustomer.Any()
                && allCustomer.All(c => c.Name != "TEKSystems"))
            {
                return await CreateCustomer("TEKSystems");
            }
            return true;
        }
    }
}