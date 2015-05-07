using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.GitLab
{
    [JsonObject]
    public class GitLabSecurityToken
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "private_token")]
        public string PrivateToken { get; set; }
    }
}
