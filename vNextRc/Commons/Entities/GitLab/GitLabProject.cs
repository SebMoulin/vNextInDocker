using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Commons.Entities.GitLab
{
    [JsonObject]
    public class GitLabProject
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "public")]
        public bool IsPublic { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "ssh_url_to_repo")]
        public string SshUrlToRepo { get; set; }
        [JsonProperty(PropertyName = "http_url_to_repo")]
        public string HttpUrlToRepo { get; set; }
        [JsonProperty(PropertyName = "web_url")]
        public string WebUrl { get; set; }
        [JsonProperty(PropertyName = "owner")]
        public GitLabUser Owner { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty(PropertyName = "last_activity_at")]
        public DateTime? LastActivityAt { get; set; }
        [JsonProperty(PropertyName = "visibility_level")]
        public string VisibilityLevel { get; set; }
        [JsonProperty(PropertyName = "import_url")]
        public string ImportUrl { get; set; }
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }
        [JsonProperty(PropertyName = "path_with_namespace")]
        public string PatWithNamespace { get; set; }
        [JsonIgnore]
        public IEnumerable<GitLabUser> Members { get; set; }
    }
}
