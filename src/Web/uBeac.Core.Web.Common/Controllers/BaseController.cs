using Microsoft.AspNetCore.Mvc;

namespace uBeac.Web
{
    [Route("API/[controller]/[action]/")]
    [ApiController]
    [Produces("application/json")]
    [TypeFilter(typeof(ExceptionHandlingFilter), Order = 0)]
    [TypeFilter(typeof(ValidationFilter), Order = 1)]
    [TypeFilter(typeof(DebugArrayFilter), Order = 2)]
    [TypeFilter(typeof(ResultFilter), Order = 3)]
    public abstract class BaseController
    {
    }
}
