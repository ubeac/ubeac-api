using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace uBeac.Auth;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IAuthService authService)
    {
        try
        {
            if (context.Request.Headers.TryGetValue("Authorization", out StringValues authHeader) &&
                authHeader.ToString().StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                var accessToken = authHeader.ToString().Substring("Bearer".Length).Trim();
                var claims = await authService.ValidateToken(accessToken);
                var identity = new ClaimsIdentity(claims, "uBeac-Auth");
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