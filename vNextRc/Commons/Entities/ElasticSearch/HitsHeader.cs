using Newtonsoft.Json;

namespace Commons.Entities.ElasticSearch
{
    [JsonObject]
    public class HitsHeader<TEntity>
    {
        [JsonProperty(PropertyName = "total")]
        public double Total { get; set; }
        [JsonProperty(PropertyName = "max_score")]
        public double? MaxScore { get; set; }
        [JsonProperty(PropertyName = "hits")]
        public Hits<TEntity>[] Hits { get; set; }
    }
}
