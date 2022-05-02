using Microsoft.AspNetCore.Http;
using System.Net;

namespace uBeac.Web;

public class ApplicationContext : IApplicationContext
{
    protected readonly IHttpContextAccessor Accessor;
    protected const string SidHeaderKey = "SID";
    protected const string UidHeaderKey = "UID";

    public ApplicationContext(IHttpContextAccessor accessor)
    {
        Accessor = accessor;

        TraceId = Accessor.HttpContext?.TraceIdentifier;
        UniqueId = Accessor.HttpContext?.Request?.Headers?.FirstOrDefault(_ => _.Key.Equals(UidHeaderKey, StringComparison.OrdinalIgnoreCase)).Value;
        SessionId = Accessor.HttpContext?.Request?.Headers?.FirstOrDefault(_ => _.Key.Equals(SidHeaderKey, StringComparison.OrdinalIgnoreCase)).Value;
        UserName = Accessor.HttpContext?.User?.Identity?.Name;
        UserIp = Accessor.HttpContext?.Connection?.RemoteIpAddress;
        Language = Accessor.HttpContext?.Request?.GetTypedHeaders().AcceptLanguage.FirstOrDefault()?.Value.Value ?? "en-US";
    }

    public string TraceId { get; set; }
    public string UniqueId { get; set; } // UID
    public string SessionId { get; set; } // SID
    public string UserName { get; set; }
    public IPAddress UserIp { get; set; }
    public string Language { get; set; }
}