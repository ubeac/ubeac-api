using System.Net;

namespace uBeac
{
    public class DummyApplicationContext : IApplicationContext
    {
        public virtual string TraceId { get; } = Guid.NewGuid().ToString();
        public virtual string SessionId { get; } = string.Empty;
        public virtual string UserName { get; } = string.Empty;
        public virtual string UserIp { get; } = IPAddress.Parse("127.0.1.1").ToString();
        public virtual string Language { get; } = "en-US";
    }
}
