using Microsoft.AspNetCore.Mvc;

namespace uBeac.Web
{
    [Route("API/[controller]/[action]/")]
    [ApiController]
    [Produces("application/json")]
    [TypeFilter(typeof(ValidationFilter), Order = 0)]
    [TypeFilter(typeof(ExceptionHandlingFilter), Order = 1)]
    [TypeFilter(typeof(ResultFilter), Order = 2)]
    public abstract class BaseController
    {
    }
}
