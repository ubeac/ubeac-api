using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace uBeac.Web;

internal static class InternalExtensions
{
    public static bool IsIResult(this ActionExecutingContext context)
        => context.Result is not null && typeof(ObjectResult) == context.Result.GetType() && ((ObjectResult)context.Result).Value is IResult;

    public static IResult GetIResult(this ActionExecutingContext context)
        => (IResult)((ObjectResult)context.Result).Value;
}