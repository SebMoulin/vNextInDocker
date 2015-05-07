using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.GitLab
{
    [JsonObject]
    public class GitLabUser
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "confirm")]
        public bool Confirm { get; set; }
        [JsonProperty(PropertyName = "can_create_group")]
        public bool CanCreateGroup { get; set; }
        [JsonProperty(PropertyName = "projects_limit")]
        public int ProjectsLimit { get; set; }
        [JsonProperty(PropertyName = "admin")]
        public bool IsAdmin { get; set; }
        [JsonProperty(PropertyName = "access_level")]
        public string AccessLevel { get; set; }
    }
}
