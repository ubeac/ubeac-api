using uBeac.Identity;
using uBeac.Identity.EntityFramework;
using uBeac.Repositories.EntityFramework;

namespace Microsoft.Extensions.DependencyInjection;

public static class UnitExtensions
{
    public static IServiceCollection AddEFUnitRepository<TContext, TUnitKey, TUnit>(this IServiceCollection services)
        where TContext : EFDbContext
        where TUnitKey : IEquatable<TUnitKey>
        where TUnit : Unit<TUnitKey>
    {
        services.AddScoped<IUnitRepository<TUnitKey, TUnit>, EFUnitRepository<TUnitKey, TUnit, TContext>>();
        return services;
    }

    public static IServiceCollection AddEFUnitRepository<TContext, TUnit>(this IServiceCollection services)
        where TContext : EFDbContext
        where TUnit : Unit
    {
        services.AddScoped<IUnitRepository<TUnit>, EFUnitRepository<TUnit, TContext>>();
        return services;
    }
}