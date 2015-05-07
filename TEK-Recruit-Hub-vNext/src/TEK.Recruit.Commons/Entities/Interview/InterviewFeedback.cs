using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.Interview
{
    [JsonObject]
    public class InterviewFeedback
    {
        [JsonProperty(PropertyName = "culturalFit")]
        public int CulturalFit { get; set; }

        [JsonProperty(PropertyName = "technicalInterview")]
        public int TechnicalInterview { get; set; }
    }
}
