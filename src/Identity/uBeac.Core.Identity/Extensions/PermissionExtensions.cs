using uBeac.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class PermissionExtensions
{
    public static IServiceCollection AddPermissionService<TPermissionService, TPermissionKey, TPermission>(this IServiceCollection services)
        where TPermissionKey : IEquatable<TPermissionKey>
        where TPermission : Permission<TPermissionKey>
        where TPermissionService : class, IPermissionService<TPermissionKey, TPermission>
    {
        services.AddScoped<IPermissionService<TPermissionKey, TPermission>, TPermissionService>();
        return services;
    }

    public static IServiceCollection AddPermissionService<TPermissionService, TPermission>(this IServiceCollection services)
        where TPermission : Permission
        where TPermissionService : class, IPermissionService<TPermission>
    {
        services.AddScoped<IPermissionService<TPermission>, TPermissionService>();
        return services;
    }
}