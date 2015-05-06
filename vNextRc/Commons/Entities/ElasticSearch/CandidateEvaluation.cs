using Newtonsoft.Json;

namespace Commons.Entities.ElasticSearch
{
    [JsonObject]
    public class CandidateEvaluation
    {
        [JsonProperty(PropertyName = "projectId")]
        public string ProjectId { get; set; }
        [JsonProperty(PropertyName = "candidateId")]
        public string CandidateId { get; set; }
        [JsonProperty(PropertyName = "codeQuality")]
        public int CodeQuality { get; set; }
        [JsonProperty(PropertyName = "culturalFit")]
        public int CulturalFit { get; set; }
        [JsonProperty(PropertyName = "technicalInterview")]
        public int TechnicalInterview { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }
        [JsonProperty(PropertyName = "tekLocation")]
        public string TekLocation { get; set; }
    }
}
