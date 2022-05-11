using System.Net;

namespace MyBlazorApp.Shared
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public T? Data { get; set; }
    }
}
