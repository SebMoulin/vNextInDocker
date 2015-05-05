using Newtonsoft.Json;

namespace Commons.Entities.ElasticSearch
{
    [JsonObject]
    public class Hits<TEntity>
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "_index")]
        public string Index { get; set; }
        [JsonProperty(PropertyName = "_type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "_score")]
        public string Score { get; set; }
        [JsonProperty(PropertyName = "_source")]
        public TEntity Source { get; set; }
    }
}
