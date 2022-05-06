using Microsoft.AspNetCore.Http;

namespace uBeac.Web;

public class Debugger : IDebugger
{
    private readonly HttpContext _context;
    private const string ITEMS_KEY = "internalDebug";

    public Debugger(IHttpContextAccessor accessor)
    {
        _context = accessor.HttpContext;
    }

    public void Add(object value)
    {
        if (!_context.Items.ContainsKey(ITEMS_KEY)) _context.Items.Add(ITEMS_KEY, new List<object>());

        var debugList = _context.Items[ITEMS_KEY] as List<object>;
        debugList!.Add(value);
    }

    public List<object> GetValues()
    {
        if (!_context.Items.ContainsKey(ITEMS_KEY)) return new List<object>();

        return _context.Items[ITEMS_KEY] as List<object>;
    }
}