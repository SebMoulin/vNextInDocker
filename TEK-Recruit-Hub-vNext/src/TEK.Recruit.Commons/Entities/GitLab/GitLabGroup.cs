using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.GitLab
{
    [JsonObject]
    public class GitLabGroup
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
