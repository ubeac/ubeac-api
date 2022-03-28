using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace uBeac.Identity;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IAuthenticator authenticator)
    {
        try
        {
            if (context.Request.Headers.TryGetValue("Authorization", out StringValues authHeader) &&
                authHeader.ToString().StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                var accessToken = authHeader.ToString().Substring("Bearer".Length).Trim();
                var claims = await authenticator.Authenticate(accessToken);
                var identity = new ClaimsIdentity(claims, "uBeac-Authentication");
                context.User = new ClaimsPrincipal(identity);
            }
        }
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        finally
        {
            await _next(context);
        }
    }
}