using System.Net;

namespace uBeac
{
    public class DummyApplicationContext<TUserKey> : IApplicationContext<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        public DummyApplicationContext()
        {
            var id = Guid.NewGuid().ToString();
            UserIp = IPAddress.Parse("127.0.1.1");
            Language = "en";
            SessionId = id;
            TraceId = id;
            Username = string.Empty;
            UserId = default;
        }

        public virtual TUserKey? UserId { get; }

        public virtual string Username { get; }

        public virtual IPAddress UserIp { get; }

        public virtual string Language { get; }

        public virtual string SessionId { get; }

        public virtual string TraceId { get; }
    }
}
