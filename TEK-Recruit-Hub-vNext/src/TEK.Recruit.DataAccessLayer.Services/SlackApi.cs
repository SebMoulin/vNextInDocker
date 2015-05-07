using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.Slack;
using TEK.Recruit.Framework.Configuration.Services;
using TEK.Recruit.Framework.Http.Services;

namespace TEK.Recruit.DataAccessLayer.Services
{
    public class SlackApi : ISlackApi
    {
        private readonly IHandleHttpRequest _handleHttpRequest;

        public SlackApi(IHandleHttpRequest handleHttpRequest, IProvideConfig configProvider)
        {
            if (handleHttpRequest == null) throw new ArgumentNullException("handleHttpRequest");
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            _handleHttpRequest = handleHttpRequest;
            _handleHttpRequest.Init(configProvider.GetSlackBaseApi(), string.Empty);
        }

        public async Task<bool> SendToSlack(string message)
        {
            var json = new SlackMessage()
            {
                Text = message
            };
            return await _handleHttpRequest.PostHttpRequest("services/T03NU4S7E/B04CYFJAP/OnhJhZZKxPcZ1bPpNHydbWHD", json, null);
        }
    }
}