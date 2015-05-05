using Newtonsoft.Json;

namespace Commons.Entities.Sonar
{
    [JsonObject]
    public class SonarMetrics
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }
        [JsonProperty(PropertyName = "qualifier")]
        public string Qualifier { get; set; }
        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }
        [JsonProperty(PropertyName = "creationDate")]
        public string CreationDate { get; set; }
        [JsonProperty(PropertyName = "lname")]
        public string Lname { get; set; }
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "msr")]
        public SonarMsr[] Msr { get; set; }
    }
}
