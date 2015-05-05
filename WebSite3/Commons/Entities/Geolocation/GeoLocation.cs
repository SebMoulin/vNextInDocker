using System.Collections.Generic;
using Newtonsoft.Json;

namespace Commons.Entities.Geolocation
{
    [JsonObject]
    public class GeoLocation
    {
        [JsonProperty(PropertyName = "response")]
        public Response Response { get; set; }
    }

    public class Doc
    {
        [JsonProperty(PropertyName = "lat")]
        public double Lat { get; set; }
        [JsonProperty(PropertyName = "lng")]
        public double Lng { get; set; }
    }

    public class Response
    {
        [JsonProperty(PropertyName = "docs")]
        public List<Doc> Docs { get; set; }
    }
}