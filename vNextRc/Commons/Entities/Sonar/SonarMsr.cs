using Newtonsoft.Json;

namespace Commons.Entities.Sonar
{
    [JsonObject]
    public class SonarMsr
    {
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
        [JsonProperty(PropertyName = "val")]
        public double Val { get; set; }
        [JsonProperty(PropertyName = "frmt_val")]
        public string FrmtVal{ get; set; }
    }
}
