using Microsoft.AspNetCore.Http.Extensions;

namespace uBeac.Web;

public class HttpLog
{
    public HttpLog(double duration, HttpRequest request, HttpResponse response, Exception? error)
    {
        Duration = duration;
        Request = request;
        Response = response;
        Error = error;
    }

    public double Duration { get; set; }
    public Exception? Error { get; set; }
    public HttpRequest Request { get; set; }
    public HttpResponse Response { get; set; }

    public class HttpRequest
    {
        public HttpRequest(Microsoft.AspNetCore.Http.HttpRequest request, string body)
        {
            DisplayUrl = request.GetDisplayUrl();
            Protocol = request.Protocol;
            Method = request.Method;
            Scheme = request.Scheme;
            PathBase = request.PathBase;
            Path = request.Path;
            QueryString = request.QueryString.Value;
            ContentType = request.ContentType;
            ContentLength = request.ContentLength;
            Headers = request.Headers.Select(_ => new KeyValuePair<string, object>(_.Key, _.Value));
            Body = body;
        }

        public string DisplayUrl { get; set; }
        public string Protocol { get; set; }
        public string Method { get; set; }
        public string Scheme { get; set; }
        public string PathBase { get; set; }
        public string Path { get; set; }
        public string? QueryString { get; set; }
        public string? ContentType { get; set; }
        public long? ContentLength { get; set; }
        public string Body { get; set; }
        public IEnumerable<KeyValuePair<string, object>> Headers { get; set; }
    }

    public class HttpResponse
    {
        public HttpResponse(Microsoft.AspNetCore.Http.HttpResponse response, string body)
        {
            ContentType = response.ContentType;
            ContentLength = response.ContentLength;
            Headers = response.Headers.Select(_ => new KeyValuePair<string, object>(_.Key, _.Value));
            StatusCode = response.StatusCode;
            Body = body;
        }

        public string ContentType { get; set; }
        public long? ContentLength { get; set; }
        public IEnumerable<KeyValuePair<string, object>> Headers { get; set; }
        public int StatusCode { get; set; }
        public string Body { get; set; }
    }
}