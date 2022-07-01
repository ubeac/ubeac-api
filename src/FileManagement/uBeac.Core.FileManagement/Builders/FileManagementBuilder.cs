using uBeac;
using uBeac.FileManagement;

namespace Microsoft.Extensions.DependencyInjection;

public class FileManagementBuilder<TKey, TEntity> : IFileManagementBuilder<TKey, TEntity> where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>, new()
{
    protected readonly IServiceCollection Services;

    public FileManagementBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IFileServiceBuilder<TKey, TEntity> AddCategory(string categoryName)
    {
        var builder = new FileServiceBuilder<TKey, TEntity>();

        Services.AddScoped(serviceProvider =>
        {
            var service = builder.Build(serviceProvider);
            return new FileCategory { CategoryName = categoryName, Service = service };
        });

        return builder;
    }
}