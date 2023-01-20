using Microsoft.AspNetCore.Identity;
using uBeac;
using uBeac.Identity;
using uBeac.Identity.Seeders;

namespace Microsoft.Extensions.DependencyInjection;

public static class UnitExtensions
{
    public static IServiceCollection AddUnitService<TUnitService, TUnitKey, TUnit>(this IServiceCollection services)
        where TUnitKey : IEquatable<TUnitKey>
        where TUnit : Unit<TUnitKey>
        where TUnitService : class, IUnitService<TUnitKey, TUnit>
    {
        services.AddScoped<IUnitService<TUnitKey, TUnit>, TUnitService>();
        return services;
    }

    public static IServiceCollection AddUnitService<TUnitService, TUnit>(this IServiceCollection services)
        where TUnit : Unit
        where TUnitService : class, IUnitService<TUnit>
    {
        services.AddScoped<IUnitService<TUnit>, TUnitService>();
        return services;
    }

    public static IdentityBuilder AddIdentityUnit<TUnitKey, TUnit>(this IdentityBuilder builder, Action<UnitOptions<TUnitKey, TUnit>> configureOptions = default)
            where TUnitKey : IEquatable<TUnitKey>
            where TUnit : Unit<TUnitKey>
    {
        if (configureOptions is not null)
        {
            // Register options
            builder.Services.RegisterUnitOptions(configureOptions);

            // Insert default values
            builder.Services.AddScoped<IDataSeeder, UnitSeeder<TUnitKey, TUnit>>();
        }

        return builder;
    }

    public static IdentityBuilder AddIdentityUnit<TUnit>(this IdentityBuilder builder, Action<UnitOptions<TUnit>> configureOptions = default)
        where TUnit : Unit
    {
        if (configureOptions is not null)
        {
            // Register options
            builder.Services.RegisterUnitOptions(configureOptions);

            // Insert default values
            builder.Services.AddScoped<IDataSeeder, UnitSeeder<TUnit>>();
        }

        return builder;
    }

    private static UnitOptions<TUnitKey, TUnit> RegisterUnitOptions<TUnitKey, TUnit>(this IServiceCollection services, Action<UnitOptions<TUnitKey, TUnit>> configureOptions)
        where TUnitKey : IEquatable<TUnitKey>
        where TUnit : Unit<TUnitKey>
    {
        var options = new UnitOptions<TUnitKey, TUnit>();
        configureOptions(options);

        // Register IOptions<UnitOptions<,>>
        services.Configure(configureOptions);
        
        // Register UnitOptions<,> without IOptions
        services.AddSingleton<UnitOptions<TUnitKey, TUnit>>(options);

        return options;
    }

    private static UnitOptions<TUnit> RegisterUnitOptions<TUnit>(this IServiceCollection services, Action<UnitOptions<TUnit>> configureOptions)
        where TUnit : Unit
    {
        var options = new UnitOptions<TUnit>();
        configureOptions(options);

        // Register IOptions<UnitOptions<,>>
        services.Configure(configureOptions);
        
        // Register UnitOptions<,> without IOptions
        services.AddSingleton<UnitOptions<TUnit>>(options);

        return options;
    }
}
