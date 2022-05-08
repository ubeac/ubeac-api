using Microsoft.AspNetCore.Http;

namespace uBeac.Web;

public class Debugger : IDebugger
{
    private const string ITEMS_KEY = "internalDebug";
    private readonly List<object> _items = new();

    public Debugger(IHttpContextAccessor httpContextAccessor)
    {
        httpContextAccessor.HttpContext.Items[ITEMS_KEY] = _items;
    }

    public void Add(object value) => _items.Add(value);

    public List<object> GetValues() => _items;
}