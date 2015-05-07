using System;
using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.Interview
{
    [JsonObject]
    public class FinalAssessment
    {
        [JsonProperty(PropertyName = "passedSelection")]
        public bool PassedTheSelection { get; set; }

        [JsonProperty(PropertyName = "passedSelectionDate")]
        public DateTime PassedTheSelectionDate { get; set; }

        [JsonProperty(PropertyName = "finalFeedback")]
        public string FinalFeedback { get; set; }
    }
}
