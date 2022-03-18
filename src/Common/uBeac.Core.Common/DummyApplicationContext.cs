using System.Net;

namespace uBeac
{
    public class DummyApplicationContext : IApplicationContext
    {
        public DummyApplicationContext()
        {
            var id = Guid.NewGuid().ToString();
            UserIp = IPAddress.Parse("127.0.1.1");
            Language = "en";
            SessionId = id;
            TraceId = id;
            UserName = string.Empty;
            Time = DateTime.Now;
        }

        public virtual string UserName { get; }
        public virtual IPAddress UserIp { get; }
        public virtual string Language { get; }
        public virtual string SessionId { get; }
        public virtual string TraceId { get; }
        public virtual DateTime Time { get; }
    }
}
