using Microsoft.AspNetCore.Http;

namespace uBeac.Web;

public class Debugger : IDebugger
{
    private const string ITEMS_KEY = "internalDebug";
    private readonly List<object> items;

    public Debugger(IHttpContextAccessor accessor)
    {
        items = new List<object>();
        accessor.HttpContext.Items[ITEMS_KEY] = items;
    }

    public void Add(object value)
    {
        items.Add(value);
    }

    public List<object> GetValues() => items;
}