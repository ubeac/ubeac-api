using Microsoft.AspNetCore.Http;
using System.Net;

namespace uBeac.Web
{
    public class ApplicationContext : IApplicationContext
    {
        public ApplicationContext(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext == null) return;

            var httpContext = httpContextAccessor.HttpContext;

            UserIp = httpContext.Connection.RemoteIpAddress;
            TraceId = httpContext.TraceIdentifier;
            Language = httpContext.Request.GetTypedHeaders().AcceptLanguage.FirstOrDefault()?.Value.Value ?? "EN";


            if (httpContext.User?.Identity != null && httpContext.User?.Claims?.Count() > 0)
            {
                UserName = httpContext.User.Identity.Name ?? string.Empty;
            }
            else
            {
                UserName = string.Empty;
            }
        }

        public IPAddress UserIp { get; }
        public string UserName { get; }
        public string Language { get; }
        public string SessionId => throw new NotImplementedException();
        public string TraceId { get; }
    }
}
