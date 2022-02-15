using System.Net;

namespace uBeac
{
    public interface IApplicationContext
    {
        public string TraceId { get; }
        public string UserName { get; }
        public IPAddress UserIp { get; }
        public string Language { get; }
        public string SessionId { get; }
    }
}
