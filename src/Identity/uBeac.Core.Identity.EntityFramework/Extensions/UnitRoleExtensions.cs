using Microsoft.EntityFrameworkCore;
using uBeac.Identity;
using uBeac.Identity.EntityFramework;
using uBeac.Repositories.EntityFramework;

namespace Microsoft.Extensions.DependencyInjection;

public static class UniTUnitRoleExtensions
{
    public static IServiceCollection AddEFUnitRoleRepository<TContext, TUnitRoleKey, TUnitRole>(this IServiceCollection services)
        where TContext : DbContext
        where TUnitRoleKey : IEquatable<TUnitRoleKey>
        where TUnitRole : UnitRole<TUnitRoleKey>
    {
        services.AddScoped<IUnitRoleRepository<TUnitRoleKey, TUnitRole>, EFUnitRoleRepository<TUnitRoleKey, TUnitRole, TContext>>();
        return services;
    }

    public static IServiceCollection AddEFUnitRoleRepository<TContext, TUnitRole>(this IServiceCollection services)
        where TContext : DbContext
        where TUnitRole : UnitRole
    {
        services.AddScoped<IUnitRoleRepository<TUnitRole>, EFUnitRoleRepository<TUnitRole, TContext>>();
        return services;
    }
}