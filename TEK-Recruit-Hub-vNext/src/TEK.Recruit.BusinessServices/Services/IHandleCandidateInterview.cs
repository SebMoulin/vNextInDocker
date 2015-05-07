using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.Interview;

namespace TEK.Recruit.BusinessServices.Services
{
    public interface IHandleCandidateInterview
    {
        Task<bool> SaveCandidateInterview(Interview interview);
        Task<Interview> GetCandidateInterviewById(string projectId, string candidateId);
    }
}
