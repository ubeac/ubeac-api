namespace uBeac;

public interface IApplicationContext
{
    public string TraceId { get; }
    public string SessionId { get; }
    public string UserName { get; }
    public string UserIp { get; }
    public string Language { get; }
}