using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PhoneBook;

public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public string UnitType { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (string.IsNullOrEmpty(UnitType)) return;

        if (context.HttpContext.User.HasClaim("unitType", UnitType) is false)
        {
            context.Result = new ForbidResult();
        }
    }
}