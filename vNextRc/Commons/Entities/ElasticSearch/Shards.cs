using Newtonsoft.Json;

namespace Commons.Entities.ElasticSearch
{
    [JsonObject]
    public class Shards
    {
        [JsonProperty(PropertyName = "total")]
        public double Total { get; set; }
        [JsonProperty(PropertyName = "successful")]
        public double Successful { get; set; }
        [JsonProperty(PropertyName = "failed")]
        public double Failed { get; set; }
    }
}
