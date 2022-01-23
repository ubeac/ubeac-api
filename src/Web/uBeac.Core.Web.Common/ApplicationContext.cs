using Microsoft.AspNetCore.Http;
using System.Net;

namespace uBeac.Web
{
    public class ApplicationContext<TUserKey> : IApplicationContext<TUserKey> where TUserKey : IEquatable<TUserKey>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var ctx = _httpContextAccessor.HttpContext;

            UserIp = ctx.Connection.RemoteIpAddress;
            TraceId = ctx.TraceIdentifier;
            Language = ctx.Request.GetTypedHeaders().AcceptLanguage.FirstOrDefault()?.Value.Value ?? "EN";


            //if (_httpContextAccessor.HttpContext?.User?.Identity != null && _httpContextAccessor.HttpContext.User?.Claims?.Count() > 0)
            //{
            //    Username = _httpContextAccessor.HttpContext.User.Identity.Name;

            //    var idClaim = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type.ToLower().Equals("id")).SingleOrDefault();

            //    if (idClaim != null)
            //        UserId = (TUserKey)TypeDescriptor.GetConverter(typeof(TUserKey)).ConvertFromInvariantString(idClaim.Value);
            //}
            //else
            //{
            //    Username = string.Empty;
            //    UserId = default;
            //}

        }

        public TUserKey UserId { get; }
        public IPAddress UserIp { get; }

        public string UserName { get; }

        public string Language { get; }

        public string SessionId => throw new NotImplementedException();

        public string TraceId { get; }
    }

}
