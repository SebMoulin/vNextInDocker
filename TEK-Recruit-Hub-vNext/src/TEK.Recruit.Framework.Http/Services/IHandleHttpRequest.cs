using System.Threading.Tasks;

namespace TEK.Recruit.Framework.Http.Services
{
    public interface IHandleHttpRequest
    {
        void Init(string restApiBaseUrl, string apiVersion);
        bool IsInitiliazed { get; }
        Task<RestApiResponse<T>> PostHttpRequest<T>(string apiPath, object json, string token);
        Task<bool> PostHttpRequest(string apiPath, object json, string token);
        Task<T> GetHttpRequest<T>(string apiPath, string token, string contentType = null);
        Task<string> GetHttpRequest(string apiPath);
        Task<bool> DeleteHttpRequest(string apiPath, string token);
        Task<RestApiResponse<T>> PutHttpRequest<T>(string apiPath, object json, string token);
    }
}
