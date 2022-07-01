using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac;
using uBeac.FileManagement;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileManagement<TKey, TEntity>(this IServiceCollection services, Action<IFileManagementBuilder<TKey, TEntity>> options)
        where TKey : IEquatable<TKey>
        where TEntity : IFileEntity<TKey>, new()
    {
        var builder = new FileManagementBuilder<TKey, TEntity>(services);
        options.Invoke(builder);
        services.TryAddScoped<IFileManager, FileManager>();
        return services;
    }

    public static IServiceCollection AddFileManagement<TEntity>(this IServiceCollection services, Action<IFileManagementBuilder<Guid, TEntity>> options)
        where TEntity : IFileEntity, new()
        => AddFileManagement<Guid, TEntity>(services, options);

    public static IServiceCollection AddFileManagement(this IServiceCollection services, Action<IFileManagementBuilder<Guid, FileEntity>> options)
        => AddFileManagement<Guid, FileEntity>(services, options);
}