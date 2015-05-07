using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TEK.Recruit.Framework.Http.Services
{
    public class HttpRequestHandler : IHandleHttpRequest
    {
        private string _apiVersion;
        private string _restApiBaseUrl;
        private bool _isInitialized;
        private const string JsonHeaderValue = "application/json";
        private const string TextPlainHeaderValue = "text/plain";
        private const string TokenParam = "private_token={0}";

        public bool IsInitiliazed { get { return _isInitialized; } }

        public HttpRequestHandler()
        {
            _isInitialized = false;
        }

        public void Init(string restApiBaseUrl, string apiVersion)
        {
            if (restApiBaseUrl == null) throw new ArgumentNullException("restApiBaseUrl");
            if (apiVersion == null) throw new ArgumentNullException("apiVersion");
            _apiVersion = apiVersion;
            _restApiBaseUrl = restApiBaseUrl;
            _isInitialized = true;
        }

        public async Task<RestApiResponse<T>> PostHttpRequest<T>(string apiPath, object json, string token)
        {
            using (var client = BaseHttpRequest())
            {
                var apiPathwithToken = GetApiPathWithToken(apiPath, token);
                HttpResponseMessage response;
                if (json == null)
                {
                    response = await client.PostAsync(apiPathwithToken, null);
                }
                else
                {
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue(JsonHeaderValue));
                    response = await client.PostAsJsonAsync(apiPathwithToken, json);
                }
                var gitLabResponse = new RestApiResponse<T>()
                {
                    StatusCode = response.StatusCode,
                };
                if (response.IsSuccessStatusCode)
                {
                    gitLabResponse.ReturnedObject = await response.Content.ReadAsAsync<T>();
                }
                return gitLabResponse;
            }
        }

        public async Task<bool> PostHttpRequest(string apiPath, object json, string token)
        {
            using (var client = BaseHttpRequest())
            {
                var apiPathwithToken = GetApiPathWithToken(apiPath, token);
                HttpResponseMessage response;
                if (json == null)
                {
                    response = await client.PostAsync(apiPathwithToken, null);
                }
                else
                {
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue(JsonHeaderValue));
                    response = await client.PostAsJsonAsync(apiPathwithToken, json);
                }
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<T> GetHttpRequest<T>(string apiPath, string token, string contentType = null)
        {
            using (var client = BaseHttpRequest())
            {
                client.DefaultRequestHeaders
                    .Accept
                    .Add(contentType != null
                        ? new MediaTypeWithQualityHeaderValue(TextPlainHeaderValue)
                        : new MediaTypeWithQualityHeaderValue(JsonHeaderValue));

                var apiPathwithToken = GetApiPathWithToken(apiPath, token);

                var response = await client.GetAsync(apiPathwithToken);
                if (response.IsSuccessStatusCode
                    && contentType == null)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                return default(T);
            }
        }

        public async Task<string> GetHttpRequest(string apiPath)
        {
            using (var client = BaseHttpRequest())
            {
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue(TextPlainHeaderValue));

                var apiPathwithToken = GetApiPathWithToken(apiPath, null);
                var response = await client.GetAsync(apiPathwithToken);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return null;
            }
        }

        public async Task<bool> DeleteHttpRequest(string apiPath, string token)
        {
            using (var client = BaseHttpRequest())
            {
                var apiPathwithToken = GetApiPathWithToken(apiPath, token);
                var response = await client.DeleteAsync(apiPathwithToken);
                return response.StatusCode == HttpStatusCode.OK 
                    || response.StatusCode == HttpStatusCode.NotFound;
            }
        }

        public async Task<RestApiResponse<T>> PutHttpRequest<T>(string apiPath, object json, string token)
        {
            using (var client = BaseHttpRequest())
            {
                var apiPathwithToken = GetApiPathWithToken(apiPath, token);

                HttpResponseMessage response;
                if (json == null)
                {
                    response = await client.PutAsync(apiPathwithToken, null);
                }
                else
                {
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue(JsonHeaderValue));
                    response = await client.PutAsJsonAsync(apiPathwithToken, json);
                }
                var gitLabResponse = new RestApiResponse<T>()
                {
                    StatusCode = response.StatusCode
                };
                if (response.IsSuccessStatusCode)
                {
                    gitLabResponse.ReturnedObject = await response.Content.ReadAsAsync<T>();
                }
                return gitLabResponse;
            }
        }

        
        private HttpClient BaseHttpRequest()
        {
            if (!_isInitialized)
                throw new HttpRequestException("REST API base url is not initialized");

            var handler = new WebRequestHandler
            {
                ServerCertificateValidationCallback = ServerCertificateValidationCallback,
            };
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(_restApiBaseUrl + _apiVersion)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            return client;
        }

        private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private string GetApiPathWithToken(string apiPath, string token)
        {
            var apiPathwithToken = apiPath;
            if (!String.IsNullOrWhiteSpace(token))
            {
                apiPathwithToken += (apiPath.Contains("?") ? "&" : "?") + String.Format(TokenParam, token);
            }
            return apiPathwithToken;
        }
    }
}