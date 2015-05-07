using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.GitLab
{
    [JsonObject]
    public class GitLabMembership
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }
        [JsonProperty(PropertyName = "access_level")]
        public string AccessLevel { get; set; }
    }
}