using System.Net;

namespace uBeac;

// Used to saving data to database or etc.
public class ApplicationContextModel : IApplicationContext
{
    public string TraceId { get; set; }
    public string SessionId { get; set; }
    public string UserName { get; set; }
    public IPAddress UserIp { get; set; }
    public string Language { get; set; }
}