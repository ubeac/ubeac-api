using Microsoft.AspNetCore.Mvc;
using uBeac.Web.Filters;

namespace uBeac.Web
{
    [Route("API/[controller]/[action]/")]
    [ApiController]
    [Produces("application/json")]
    [TypeFilter(typeof(ModelStateValidationFilter))]
    [TypeFilter(typeof(ResponseDebuggerFilter))]
    [TypeFilter(typeof(ApiResultFilter))]
    public abstract class BaseController
    {
    }
}
