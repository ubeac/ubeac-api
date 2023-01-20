﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using uBeac;
using uBeac.Identity;
using uBeac.Identity.Seeders;

namespace Microsoft.Extensions.DependencyInjection;

public static class UserExtensions
{
    public static IServiceCollection AddUserService<TUserService, TUserKey, TUser>(this IServiceCollection services)
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
        where TUserService : class, IUserService<TUserKey, TUser>
    {
        services.AddScoped<IUserService<TUserKey, TUser>, TUserService>();
        return services;
    }

    public static IServiceCollection AddUserService<TUserService, TUser>(this IServiceCollection services, IConfiguration config)
        where TUser : User
        where TUserService : class, IUserService<TUser>
    {
        services.Configure<UserRegisterOptions>(config.GetSection("UserRegisterOptions"));
        services.AddScoped<IUserService<TUser>, TUserService>();
        return services;
    }

    public static IdentityBuilder AddIdentityUser<TUserKey, TUser>(this IServiceCollection services, Action<IdentityOptions> configureIdentityOptions = default, Action<UserOptions<TUserKey, TUser>> configureOptions = default)
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        // Configure AspNetIdentity
        services.AddDataProtection();
        var builder = services.AddIdentityCore<TUser>(configureIdentityOptions ?? (_ => GetDefaultOptions()));
        builder
            .AddUserStore<UserStore<TUser, TUserKey>>()
            .AddUserManager<UserManager<TUser>>()
            .AddDefaultTokenProviders();

        if (configureOptions is not null)
        {
            // Register options
            builder.Services.RegisterUserOptions(configureOptions);

            // Insert default values
            builder.Services.AddScoped<IDataSeeder, UserSeeder<TUserKey, TUser>>();
        }

        return builder;
    }

    public static IdentityBuilder AddIdentityUser<TUser>(this IServiceCollection services, Action<IdentityOptions> configureIdentityOptions = default, Action<UserOptions<TUser>> configureOptions = default)
           where TUser : User
    {
        // Configure AspNetIdentity
        services.AddDataProtection();
        var builder = services.AddIdentityCore<TUser>(configureIdentityOptions ?? (_ => GetDefaultOptions()));
        builder
            .AddUserStore<UserStore<TUser>>()
            .AddUserManager<UserManager<TUser>>()
            .AddDefaultTokenProviders();

        if (configureOptions is not null)
        {
            // Register options
            builder.Services.RegisterUserOptions(configureOptions);

            // Insert default values
            builder.Services.AddScoped<IDataSeeder, UserSeeder<TUser>>();
        }

        return builder;
    }

    private static IdentityOptions GetDefaultOptions()
    {
        var options = new IdentityOptions
        {
            Password =
            {
                // Password settings
                RequireDigit = false,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequireUppercase = false,
                RequiredLength = 1,
                RequiredUniqueChars = 0
            },
            Lockout =
            {
                // Lockout settings
                DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1),
                MaxFailedAccessAttempts = 5,
                AllowedForNewUsers = true
            },
            User =
            {
                // User settings
                AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+",
                RequireUniqueEmail = true
            }
        };

        return options;
    }

    private static UserOptions<TUserKey, TUser> RegisterUserOptions<TUserKey, TUser>(this IServiceCollection services, Action<UserOptions<TUserKey, TUser>> configureOptions)
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        var options = new UserOptions<TUserKey, TUser>();
        configureOptions(options);

        // Register IOptions<UserOptions<,>>
        services.Configure(configureOptions);

        // Register UserOptions<,> without IOptions
        services.AddSingleton<UserOptions<TUserKey, TUser>>(options);

        return options;
    }

    private static UserOptions<TUser> RegisterUserOptions<TUser>(this IServiceCollection services, Action<UserOptions<TUser>> configureOptions)
        where TUser : User
    {
        var options = new UserOptions<TUser>();
        configureOptions(options);

        // Register IOptions<UserOptions<,>>
        services.Configure(configureOptions);

        // Register UserOptions<,> without IOptions
        services.AddSingleton<UserOptions<TUser>>(options);

        return options;
    }
}