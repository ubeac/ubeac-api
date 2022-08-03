using Microsoft.Extensions.Caching.Memory;

namespace uBeac.Web.Logging.MongoDB;

public class HttpLogCache
{    
    public MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());
}
