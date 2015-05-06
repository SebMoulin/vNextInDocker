using System.Net;

namespace Framework.Http
{
    public class RestApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T ReturnedObject { get; set; }
        public string Message { get; set; }

        public bool SuccessfullyCreated
        {
            get { return StatusCode == HttpStatusCode.Created; }
        }

        public bool SuccessfulRequest
        {
            get { return StatusCode == HttpStatusCode.OK; }
        }
    }
}