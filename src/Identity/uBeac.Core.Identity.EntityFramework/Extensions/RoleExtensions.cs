using Microsoft.EntityFrameworkCore;
using uBeac.Identity;
using uBeac.Identity.EntityFramework;
using uBeac.Repositories.EntityFramework;

namespace Microsoft.Extensions.DependencyInjection;

public static class RoleExtensions
{
    public static IServiceCollection AddEFRoleRepository<TContext, TRoleKey, TRole>(this IServiceCollection services)
        where TContext : DbContext
        where TRoleKey : IEquatable<TRoleKey>
        where TRole : Role<TRoleKey>
    {
        services.AddScoped<IRoleRepository<TRoleKey, TRole>, EFRoleRepository<TRoleKey, TRole, TContext>>();
        return services;
    }

    public static IServiceCollection AddEFRoleRepository<TContext, TRole>(this IServiceCollection services)
        where TContext : DbContext
        where TRole : Role
    {
        services.AddScoped<IRoleRepository<TRole>, EFRoleRepository<TRole, TContext>>();
        return services;
    }
}