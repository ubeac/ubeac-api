using Microsoft.AspNetCore.Http;
using System.Net;

namespace uBeac.Web;

public class ApplicationContext : IApplicationContext
{
    protected readonly IHttpContextAccessor Accessor;

    public ApplicationContext(IHttpContextAccessor accessor)
    {
        Accessor = accessor;
    }

    public virtual string TraceId => Accessor.HttpContext?.TraceIdentifier;
    public virtual string SessionId => Accessor.HttpContext?.TraceIdentifier;
    public virtual string UserName => Accessor.HttpContext?.User?.Identity?.Name;
    public virtual IPAddress UserIp => Accessor.HttpContext?.Connection?.RemoteIpAddress;
    public virtual string Language => Accessor.HttpContext?.Request?.GetTypedHeaders().AcceptLanguage.FirstOrDefault()?.Value.Value ?? "en-US";
}