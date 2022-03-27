using System.Net;

namespace uBeac
{
    public class DummyApplicationContext : IApplicationContext
    {
        public virtual string TraceId { get; } = Guid.NewGuid().ToString();
        public virtual string SessionId { get; } = string.Empty;
        public virtual DateTime Time { get; } = DateTime.Now;
        public virtual string UserName { get; } = string.Empty;
        public virtual IPAddress UserIp { get; } = IPAddress.Parse("127.0.1.1");
        public virtual string Language { get; } = "en-US";
    }
}
