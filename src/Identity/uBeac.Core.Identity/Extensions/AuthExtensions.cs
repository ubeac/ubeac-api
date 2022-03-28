using uBeac.Auth;
using uBeac.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class AuthExtensions
{
    public static IServiceCollection AddLocalAuthService<TUserKey, TUser>(this IServiceCollection services)
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        services.AddScoped<IAuthService, LocalAuthService<TUserKey, TUser>>();
        return services;
    }

    public static IServiceCollection AddLocalAuthService<TUser>(this IServiceCollection services)
        where TUser : User
    {
        services.AddScoped<IAuthService, LocalAuthService<TUser>>();
        return services;
    }
}