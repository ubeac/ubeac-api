using Microsoft.AspNetCore.Mvc;

namespace uBeac.Web;

[Route("API/[controller]/[action]/")]
[ApiController]
[Produces("application/json")]
[TypeFilter(typeof(ResultFilter), Order = 0)]
[TypeFilter(typeof(ModelStateValidationFilter), Order = 1)]
public abstract class BaseController
{
}