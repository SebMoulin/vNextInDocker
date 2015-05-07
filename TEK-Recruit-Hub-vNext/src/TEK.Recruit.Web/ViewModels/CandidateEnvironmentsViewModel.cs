using System.Collections.Generic;
using TEK.Recruit.Commons.Dtos;

namespace TEK.Recruit.Web.ViewModels
{
    public class CandidateEnvironmentsViewModel
    {
        public IEnumerable<CandidateEnvironmentDto> CandidateEnvironments { get; set; }
    }
}