using Microsoft.AspNetCore.Mvc;
using uBeac.Web.Filters;

namespace uBeac.Web
{
    [Route("API/[controller]/[action]/")]
    //[Authorize]
    [ApiController]
    [Produces("application/json")]
    //[SwaggerResponse(200, "OK")]
    //[SwaggerResponse(400, "Bad Request")]
    //[SwaggerResponse(401, "Unauthorized")]
    //[SwaggerResponse(403, "Forbidden")]
    //[SwaggerResponse(404, "Not Found")]
    //[SwaggerResponse(500, "Unhandled Exception")]
    [TypeFilter(typeof(ModelStateValidationFilter))]
    [TypeFilter(typeof(ApiResultFilter))]
    public abstract class BaseController
    {
    }
}
