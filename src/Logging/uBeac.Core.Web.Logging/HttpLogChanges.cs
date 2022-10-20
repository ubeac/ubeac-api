namespace uBeac.Web.Logging
{
    public interface IHttpLogChanges : IDictionary<string, object>
    {

    }
    internal class HttpLogChanges : Dictionary<string, object>, IHttpLogChanges
    {
    }
}
