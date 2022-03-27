using Microsoft.AspNetCore.Mvc;
using uBeac.Web.Filters;

namespace uBeac.Web
{
    [Route("API/[controller]/[action]/")]
    [ApiController]
    [Produces("application/json")]
    [TypeFilter(typeof(ValidationFilter))]
    [TypeFilter(typeof(DebuggingFilter))]
    [TypeFilter(typeof(ResultFilter))]
    public abstract class BaseController
    {
    }
}
