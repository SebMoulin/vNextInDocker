using System;
using System.Linq;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.Interview;
using TEK.Recruit.DataAccessLayer.Services;

namespace TEK.Recruit.BusinessServices.Services
{
    public class CandidateInterviewService : IHandleCandidateInterview
    {
        private readonly IElasticSearhApi _elasticSearhApi;

        public CandidateInterviewService(IElasticSearhApi elasticSearhApi)
        {
            if (elasticSearhApi == null) throw new ArgumentNullException("elasticSearhApi");
            _elasticSearhApi = elasticSearhApi;
        }

        public async Task<bool> SaveCandidateInterview(Interview interview)
        {
            if (interview.InterviewEyeball.PassedTheSelection)
            {
                interview.InterviewEyeball.PassedTheSelectionDate = DateTime.Now;
            }
            if (interview.FinalAssement.PassedTheSelection)
            {
                interview.FinalAssement.PassedTheSelectionDate = DateTime.Now;
            }

            var elasticSearchRecordId = await GetCandidateEvaluationElasticSearchRecordId(interview.CandidateProfile.ProjectId, interview.CandidateProfile.CandidateId);
            var response = elasticSearchRecordId != null
                ? await _elasticSearhApi.UpdateCandidateEvaluation(elasticSearchRecordId, interview)
                : await _elasticSearhApi.CreateCandidateEvaluation(interview);
            return response.SuccessfullyCreated;
        }

        public async Task<Interview> GetCandidateInterviewById(string projectId, string candidateId)
        {
            var interviewResponse = await _elasticSearhApi.GetCandidateInterview(projectId, candidateId);

            var interview = !interviewResponse.HitsHeader.Total.Equals(1)
                ? null
                : interviewResponse.HitsHeader.Hits.First().Source;
            return interview;
        }

        private async Task<string> GetCandidateEvaluationElasticSearchRecordId(string projectId, string candidateId)
        {
            string elasticSearchRecordId = null;
            var getCandidateEvalResponse = await _elasticSearhApi.GetCandidateInterview(projectId, candidateId);

            if (getCandidateEvalResponse != null
                && getCandidateEvalResponse.HitsHeader.Total.Equals(1))
            {
                elasticSearchRecordId = getCandidateEvalResponse.HitsHeader.Hits.First().Id;
            }

            return elasticSearchRecordId;
        }

    }
}
