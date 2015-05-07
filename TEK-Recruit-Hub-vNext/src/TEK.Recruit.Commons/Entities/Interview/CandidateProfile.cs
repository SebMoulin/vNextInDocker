using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.Interview
{
    [JsonObject]
    public class CandidateProfile
    {
        [JsonProperty(PropertyName = "projectId")]
        public string ProjectId { get; set; }

        [JsonProperty(PropertyName = "candidateId")]
        public string CandidateId { get; set; }

        [JsonProperty(PropertyName = "devEnv")]
        public string DevEnv { get; set; }

        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "tekLocation")]
        public string TEKCenter { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }
    }
}
