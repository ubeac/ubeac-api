using Microsoft.AspNetCore.Identity;
using uBeac;
using uBeac.Identity;
using uBeac.Identity.Seeders;

namespace Microsoft.Extensions.DependencyInjection;

public static class RoleExtensions
{
    public static IServiceCollection AddRoleService<TRoleService, TRoleKey, TRole>(this IServiceCollection services)
        where TRoleKey : IEquatable<TRoleKey>
        where TRole : Role<TRoleKey>
        where TRoleService : class, IRoleService<TRoleKey, TRole>
    {
        services.AddScoped<IRoleService<TRoleKey, TRole>, TRoleService>();
        return services;
    }

    public static IServiceCollection AddRoleService<TRoleService, TRole>(this IServiceCollection services)
        where TRole : Role
        where TRoleService : class, IRoleService<TRole>
    {
        services.AddScoped<IRoleService<TRole>, TRoleService>();
        return services;
    }

    public static IdentityBuilder AddIdentityRole<TRoleKey, TRole>(this IdentityBuilder builder, Action<RoleOptions<TRoleKey, TRole>> configureOptions = default)
            where TRoleKey : IEquatable<TRoleKey>
            where TRole : Role<TRoleKey>
    {
        // Configure AspNetIdentity
        builder
            .AddRoles<TRole>()
            .AddRoleStore<RoleStore<TRole, TRoleKey>>()
            .AddRoleManager<RoleManager<TRole>>();

        if (configureOptions is not null)
        {
            // Register options
            builder.Services.RegisterRoleOptions(configureOptions);

            // Insert default values
            builder.Services.AddScoped<IDataSeeder, RoleSeeder<TRoleKey, TRole>>();
        }

        return builder;
    }

    public static IdentityBuilder AddIdentityRole<TRole>(this IdentityBuilder builder, Action<RoleOptions<TRole>> configureOptions = default)
        where TRole : Role
    {
        // Configure AspNetIdentity
        builder
            .AddRoles<TRole>()
            .AddRoleStore<RoleStore<TRole>>()
            .AddRoleManager<RoleManager<TRole>>();

        if (configureOptions is not null)
        {
            // Register options
            builder.Services.RegisterRoleOptions(configureOptions);
            
            // Insert default values
            builder.Services.AddScoped<IDataSeeder, RoleSeeder<TRole>>();
        }

        return builder;
    }

    private static RoleOptions<TRoleKey, TRole> RegisterRoleOptions<TRoleKey, TRole>(this IServiceCollection services, Action<RoleOptions<TRoleKey, TRole>> configureOptions)
        where TRoleKey : IEquatable<TRoleKey>
        where TRole : Role<TRoleKey>
    {
        var options = new RoleOptions<TRoleKey, TRole>();
        configureOptions(options);
        
        // Register IOptions<RoleOptions<,>>
        services.Configure(configureOptions);

        // Register RoleOptions<,> without IOptions
        services.AddSingleton<RoleOptions<TRoleKey, TRole>>(options);

        return options;
    }

    private static RoleOptions<TRole> RegisterRoleOptions<TRole>(this IServiceCollection services, Action<RoleOptions<TRole>> configureOptions)
        where TRole : Role
    {
        var options = new RoleOptions<TRole>();
        configureOptions(options);
        
        // Register IOptions<RoleOptions<,>>
        services.Configure(configureOptions);

        // Register RoleOptions<,> without IOptions
        services.AddSingleton<RoleOptions<TRole>>(options);

        return options;
    }
}
