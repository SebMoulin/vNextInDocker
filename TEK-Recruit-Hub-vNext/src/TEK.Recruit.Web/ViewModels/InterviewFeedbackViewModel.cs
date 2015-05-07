using System.ComponentModel;

namespace TEK.Recruit.Web.ViewModels
{
    public class InterviewFeedbackViewModel
    {
        [DisplayName("Cultural Fit")]
        public int CulturalFit { get; set; }
        [DisplayName("Technical Interview")]
        public int TechnicalInterview { get; set; }
    }
}
