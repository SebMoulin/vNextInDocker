namespace TEK.Recruit.Commons.Dtos
{
    public class InterviewDto
    {
        public string CustomerId { get; set; }
        
        public string CustomerName { get; set; }

        public CandidateProfileDto CandidateProfile { get; set; }

        public InterviewEyeballDto InterviewEyeball { get; set; }

        public InterviewFeedbackDto InterviewFeedback { get; set; }

        public FinalAssessmentDto FinalAssement { get; set; }
    }
}
