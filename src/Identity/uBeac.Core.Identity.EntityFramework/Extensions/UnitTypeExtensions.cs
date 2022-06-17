using Microsoft.EntityFrameworkCore;
using uBeac.Identity;
using uBeac.Identity.EntityFramework;
using uBeac.Repositories.EntityFramework;

namespace Microsoft.Extensions.DependencyInjection;

public static class UnitTypeExtensions
{
    public static IServiceCollection AddEFUnitTypeRepository<TContext, TUnitTypeKey, TUnitType>(this IServiceCollection services)
        where TContext : DbContext
        where TUnitTypeKey : IEquatable<TUnitTypeKey>
        where TUnitType : UnitType<TUnitTypeKey>
    {
        services.AddScoped<IUnitTypeRepository<TUnitTypeKey, TUnitType>, EFUnitTypeRepository<TUnitTypeKey, TUnitType, TContext>>();
        return services;
    }

    public static IServiceCollection AddEFUnitTypeRepository<TContext, TUnitType>(this IServiceCollection services)
        where TContext : DbContext
        where TUnitType : UnitType
    {
        services.AddScoped<IUnitTypeRepository<TUnitType>, EFUnitTypeRepository<TUnitType, TContext>>();
        return services;
    }
}