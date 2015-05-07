using System;
using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.Interview
{
    [JsonObject]
    public class InterviewEyeball
    {
        [JsonProperty(PropertyName = "passedSelection")]
        public bool PassedTheSelection { get; set; }

        [JsonProperty(PropertyName = "passedSelectionDate")]
        public DateTime PassedTheSelectionDate { get; set; }

        [JsonProperty(PropertyName = "codeQuality")]
        public int CodeQuality { get; set; }

        [JsonProperty(PropertyName = "agenda")]
        public string Agenda { get; set; }
    }
}
