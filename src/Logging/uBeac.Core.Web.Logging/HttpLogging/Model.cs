namespace uBeac.Web;

public class HttpLog
{
    public double Duration { get; set; }
    public Exception Error { get; set; }
    public HttpRequest Request { get; set; } = new();
    public HttpResponse Response { get; set; } = new();

    public class HttpRequest
    {
        public string DisplayUrl { get; set; }
        public string Protocol { get; set; }
        public string Method { get; set; }
        public string Scheme { get; set; }
        public string PathBase { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string ContentType { get; set; }
        public long? ContentLength { get; set; }
        public string Body { get; set; }
        public IEnumerable<KeyValuePair<string, object>> Headers { get; set; }
    }

    public class HttpResponse
    {
        public string ContentType { get; set; }
        public long? ContentLength { get; set; }
        public string Body { get; set; }
        public IEnumerable<KeyValuePair<string, object>> Headers { get; set; }
        public int StatusCode { get; set; }
    }
}