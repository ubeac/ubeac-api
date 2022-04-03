using Microsoft.AspNetCore.Mvc;

namespace uBeac.Web;

[Route("API/[controller]/[action]/")]
[ApiController]
[Produces("application/json")]
[TypeFilter(typeof(ExceptionHandlingFilter))]
[TypeFilter(typeof(ResultFilter), Order = 0)]
[TypeFilter(typeof(ModelStateValidationValidationFilter), Order = 1)]
public abstract class BaseController
{
}

