using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.ElasticSearch
{
    [JsonObject]
    public class ElasticSearchQuery
    {
        [JsonProperty(PropertyName = "query")]
        public Query Query { get; set; }
    }

    public class Query
    {
        [JsonProperty(PropertyName = "query_string")]
        public QueryString QueryString { get; set; }
    }

    [JsonObject]
    public class QueryString
    {
        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }
        [JsonProperty(PropertyName = "fields")]
        public string[] Fields { get; set; }
    }
}
