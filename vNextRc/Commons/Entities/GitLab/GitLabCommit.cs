using System;
using Newtonsoft.Json;

namespace Commons.Entities.GitLab
{
    [JsonObject]
    public class GitLabCommit
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "short_id")]
        public string ShortId { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "author_name")]
        public string AuthorName { get; set; }
        [JsonProperty(PropertyName = "author_email")]
        public string AuthorEmail { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
