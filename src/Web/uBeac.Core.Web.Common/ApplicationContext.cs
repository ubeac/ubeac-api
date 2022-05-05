﻿using Microsoft.AspNetCore.Http;

namespace uBeac.Web;

public class ApplicationContext : IApplicationContext
{
    protected readonly IHttpContextAccessor Accessor;

    public ApplicationContext(IHttpContextAccessor accessor)
    {
        Accessor = accessor;

        TraceId = Accessor.HttpContext?.TraceIdentifier;
        SessionId = Accessor.HttpContext?.TraceIdentifier;
        UserName = Accessor.HttpContext?.User?.Identity?.Name;
        UserIp = Accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        Language = Accessor.HttpContext?.Request?.GetTypedHeaders().AcceptLanguage.FirstOrDefault()?.Value.Value ?? "en-US";
    }

    public string TraceId { get; set; }
    public string SessionId { get; set; }
    public string UserName { get; set; }
    public string UserIp { get; set; }
    public string Language { get; set; }
}