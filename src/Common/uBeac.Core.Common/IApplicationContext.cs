using System.Net;

namespace uBeac
{
    public interface IApplicationContext<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        public string TraceId { get; }
        public TUserKey? UserId { get; }
        public string UserName { get; }
        public IPAddress UserIp { get; }
        public string Language { get; }
        public string SessionId { get; }
    }
}
