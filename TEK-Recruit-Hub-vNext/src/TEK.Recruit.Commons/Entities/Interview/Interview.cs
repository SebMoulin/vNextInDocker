
using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.Interview
{
    [JsonObject]
    public class Interview
    {
        [JsonProperty(PropertyName = "candidateProfile")]
        public CandidateProfile CandidateProfile { get; set; }

        [JsonProperty(PropertyName = "interviewEyeball")]
        public InterviewEyeball InterviewEyeball { get; set; }

        [JsonProperty(PropertyName = "interviewFeedback")]
        public InterviewFeedback InterviewFeedback { get; set; }

        [JsonProperty(PropertyName = "finalAssement")]
        public FinalAssessment FinalAssement { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        public string CustomerId { get; set; }

        [JsonProperty(PropertyName = "customerName")]
        public string CustomerName { get; set; }
    }
}
