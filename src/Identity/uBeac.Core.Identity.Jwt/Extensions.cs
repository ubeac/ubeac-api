using Microsoft.Extensions.Configuration;
using uBeac.Identity;
using uBeac.Identity.Jwt;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddJwtService<TUserKey, TUser>(this IServiceCollection services, IConfiguration config)
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        services.Configure<JwtOptions>(config.GetSection("Jwt"));

        services.AddScoped<ITokenService<TUserKey, TUser>, JwtTokenService<TUserKey, TUser>>();
        services.AddScoped<IJwtTokenService<TUserKey, TUser>, JwtTokenService<TUserKey, TUser>>();

        services.AddScoped<UserJwtClaimsManager<TUserKey, TUser>>();
        services.AddScoped<IUserJwtClaimsDecorator<TUserKey, TUser>, DefaultUserJwtClaimsDecorator<TUserKey, TUser>>();

        return services;
    }

    public static IServiceCollection AddJwtService<TUser>(this IServiceCollection services, IConfiguration config, IUserJwtClaimsDecorator<TUser> claimsDecorator = null)
        where TUser : User
    {
        services.Configure<JwtOptions>(config.GetSection("Jwt"));

        services.AddScoped<ITokenService<TUser>, JwtTokenService<TUser>>();
        services.AddScoped<IJwtTokenService<TUser>, JwtTokenService<TUser>>();

        services.AddScoped<UserJwtClaimsManager<TUser>>();
        services.AddScoped<IUserJwtClaimsDecorator<TUser>, DefaultUserJwtClaimsDecorator<TUser>>();

        return services;
    }

    public static IServiceCollection AddJwtClaimsDecorator<TUserKey, TUser, TDecorator>(this IServiceCollection services)
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
        where TDecorator : class, IUserJwtClaimsDecorator<TUserKey, TUser>
    {
        services.AddScoped<IUserJwtClaimsDecorator<TUserKey, TUser>, TDecorator>();

        return services;
    }

    public static IServiceCollection AddJwtClaimsDecorator<TUser, TDecorator>(this IServiceCollection services)
        where TUser : User
        where TDecorator : class, IUserJwtClaimsDecorator<TUser>
    {
        services.AddScoped<IUserJwtClaimsDecorator<TUser>, TDecorator>();

        return services;
    }
}
