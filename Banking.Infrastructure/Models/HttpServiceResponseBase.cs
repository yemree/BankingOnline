using System.Net;

namespace Banking.Infrastructure.Models
{
    public class HttpServiceResponseBase
    {
        public ErrorModel Error { get; set; }
    }

    public class HttpServiceResponseBase<TResponse>
    {
        public TResponse Data { get; set; }
        public HttpStatusCode Code { get; set; }
    }

    public class ErrorModel
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
