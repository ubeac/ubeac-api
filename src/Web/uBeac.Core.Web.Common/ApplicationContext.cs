using Microsoft.AspNetCore.Http;
using System.Net;

namespace uBeac.Web;

public class ApplicationContext : IApplicationContext
{
    public ApplicationContext(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext == null) return;

        var httpContext = httpContextAccessor.HttpContext;
        TraceId = httpContext.TraceIdentifier;
        SessionId = TraceId;
        UserIp = httpContext.Connection.RemoteIpAddress;
        Language = httpContext.Request.GetTypedHeaders().AcceptLanguage.FirstOrDefault()?.Value.Value ?? "en-US";
        UserName = httpContext.User?.Identity?.Name ?? string.Empty;
    }

    public virtual string TraceId { get; }
    public virtual string SessionId { get; }
    public virtual string UserName { get; }
    public virtual IPAddress UserIp { get; }
    public virtual string Language { get; }
}