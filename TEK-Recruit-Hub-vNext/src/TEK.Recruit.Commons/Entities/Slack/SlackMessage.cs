using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.Slack
{
    [JsonObject]
    public class SlackMessage
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}
