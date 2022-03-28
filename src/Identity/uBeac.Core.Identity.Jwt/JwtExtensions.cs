using uBeac.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtService<TUserKey, TUser>(this IServiceCollection services, JwtOptions options)
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        services.AddSingleton(options);

        services.AddScoped<ITokenService<TUserKey, TUser>, JwtTokenService<TUserKey, TUser>>();
        services.AddScoped<IJwtTokenService<TUserKey, TUser>, JwtTokenService<TUserKey, TUser>>();

        return services;
    }

    public static IServiceCollection AddJwtService<TUser>(this IServiceCollection services, JwtOptions options)
        where TUser : User
    {
        services.AddSingleton(options);

        services.AddScoped<ITokenService<TUser>, JwtTokenService<TUser>>();
        services.AddScoped<IJwtTokenService<TUser>, JwtTokenService<TUser>>();

        return services;
    }
}