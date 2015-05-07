namespace TEK.Recruit.Web.ViewModels
{
    public class CandidateEvaluationFormViewModel
    {
        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public CandidateProfileViewModel CandidateProfile { get; set; }

        public InterviewEyeballViewModel InterviewEyeball { get; set; }

        public InterviewFeedbackViewModel InterviewFeedback { get; set; }

        public FinalAssessmentViewModel FinalAssement { get; set; }
    }
}