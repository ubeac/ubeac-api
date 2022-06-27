using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac;
using uBeac.FileManagement;

namespace Microsoft.Extensions.DependencyInjection;

public class FileManagementBuilder<TKey, TEntity> : IFileManagementBuilder<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
{
    protected readonly IServiceCollection Services;

    public FileManagementBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IFileServiceBuilder<TKey, TEntity> AddCategory(string categoryName)
    {
        var builder = new FileServiceBuilder<TKey, TEntity>();

        Services.AddScoped<IFileCategory>(serviceProvider =>
        {
            var service = builder.Build(serviceProvider);
            return new FileCategory { CategoryName = categoryName, Service = service };
        });

        return builder;
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileManagement<TKey, TEntity>(this IServiceCollection services, Action<IFileManagementBuilder<TKey, TEntity>> options)
        where TKey : IEquatable<TKey>
        where TEntity : IFileEntity<TKey>
    {
        var builder = new FileManagementBuilder<TKey, TEntity>(services);
        options.Invoke(builder);

        services.TryAddScoped<IFileManager, FileManager>();

        return services;
    }

    public static IServiceCollection AddFileManagement<TEntity>(this IServiceCollection services, Action<IFileManagementBuilder<Guid, TEntity>> options)
        where TEntity : IFileEntity
        => AddFileManagement<Guid, TEntity>(services, options);

    public static IServiceCollection AddFileManagement(this IServiceCollection services, Action<IFileManagementBuilder<Guid, FileEntity>> options)
        => AddFileManagement<Guid, FileEntity>(services, options);
}