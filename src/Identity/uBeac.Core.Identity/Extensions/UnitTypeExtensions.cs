using Microsoft.AspNetCore.Identity;
using uBeac;
using uBeac.Identity;
using uBeac.Identity.Seeders;

namespace Microsoft.Extensions.DependencyInjection;

public static class UnitTypeExtensions
{
    public static IServiceCollection AddUnitTypeService<TUnitTypeService, TUnitTypeKey, TUnitType>(this IServiceCollection services)
        where TUnitTypeKey : IEquatable<TUnitTypeKey>
        where TUnitType : UnitType<TUnitTypeKey>
        where TUnitTypeService : class, IUnitTypeService<TUnitTypeKey, TUnitType>
    {
        services.AddScoped<IUnitTypeService<TUnitTypeKey, TUnitType>, TUnitTypeService>();
        return services;
    }

    public static IServiceCollection AddUnitTypeService<TUnitTypeService, TUnitType>(this IServiceCollection services)
        where TUnitType : UnitType
        where TUnitTypeService : class, IUnitTypeService<TUnitType>
    {
        services.AddScoped<IUnitTypeService<TUnitType>, TUnitTypeService>();
        return services;
    }

    public static IdentityBuilder AddIdentityUnitType<TUnitTypeKey, TUnitType>(this IdentityBuilder builder, Action<UnitTypeOptions<TUnitTypeKey, TUnitType>> configureOptions = default)
        where TUnitTypeKey : IEquatable<TUnitTypeKey>
        where TUnitType : UnitType<TUnitTypeKey>
    {
        if (configureOptions is not null)
        {
            // Register options
            builder.Services.RegisterUnitTypeOptions(configureOptions);

            // Insert default values
            builder.Services.AddScoped<IDataSeeder, UnitTypeSeeder<TUnitTypeKey, TUnitType>>();
        }

        return builder;
    }

    public static IdentityBuilder AddIdentityUnitType<TUnitType>(this IdentityBuilder builder, Action<UnitTypeOptions<TUnitType>> configureOptions = default)
        where TUnitType : UnitType
    {
        if (configureOptions is not null)
        {
            // Register options
            builder.Services.RegisterUnitTypeOptions(configureOptions);

            // Insert default values
            builder.Services.AddScoped<IDataSeeder, UnitTypeSeeder<TUnitType>>();
        }

        return builder;
    }

    private static UnitTypeOptions<TUnitTypeKey, TUnitType> RegisterUnitTypeOptions<TUnitTypeKey, TUnitType>(this IServiceCollection services, Action<UnitTypeOptions<TUnitTypeKey, TUnitType>> configureOptions)
        where TUnitTypeKey : IEquatable<TUnitTypeKey>
        where TUnitType : UnitType<TUnitTypeKey>
    {
        var options = new UnitTypeOptions<TUnitTypeKey, TUnitType>();
        configureOptions(options);

        // Register IOptions<UnitTypeOptions<,>>
        services.Configure(configureOptions);

        // Register UnitTypeOptions<,> without IOptions
        services.AddSingleton<UnitTypeOptions<TUnitTypeKey, TUnitType>>(options);

        return options;
    }

    private static UnitTypeOptions<TUnitType> RegisterUnitTypeOptions<TUnitType>(this IServiceCollection services, Action<UnitTypeOptions<TUnitType>> configureOptions)
        where TUnitType : UnitType
    {
        var options = new UnitTypeOptions<TUnitType>();
        configureOptions(options);

        // Register IOptions<UnitTypeOptions<,>>
        services.Configure(configureOptions);

        // Register UnitTypeOptions<,> without IOptions
        services.AddSingleton<UnitTypeOptions<TUnitType>>(options);

        return options;
    }
}