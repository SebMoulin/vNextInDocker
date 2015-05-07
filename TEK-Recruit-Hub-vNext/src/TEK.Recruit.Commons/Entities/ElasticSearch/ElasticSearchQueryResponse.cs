using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.ElasticSearch
{
    [JsonObject]
    public class ElasticSearchQueryResponse<TEntity>
{
        [JsonProperty(PropertyName = "took")]
        public double Took { get; set; }
        [JsonProperty(PropertyName = "timed_out")]
        public bool TimedOut { get; set; }
        [JsonProperty(PropertyName = "_shards")]
        public Shards Shards { get; set; }
        [JsonProperty(PropertyName = "hits")]
        public HitsHeader<TEntity> HitsHeader { get; set; }
    }
}
