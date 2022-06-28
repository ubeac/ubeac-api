using uBeac;
using uBeac.FileManagement;
using uBeac.FileManagement.LocalStorage;

namespace Microsoft.Extensions.DependencyInjection;

public static class FileServiceBuilderExtensions
{
    public static IFileServiceBuilder<TKey, TEntity> StoreFilesInLocalDisk<TKey, TEntity>(this IFileServiceBuilder<TKey, TEntity> builder, FileManagementLocalDiskOptions options)
        where TKey : IEquatable<TKey>
        where TEntity : IFileEntity<TKey>
    {
        builder.Provider = serviceProvider => ActivatorUtilities.CreateInstance<LocalDiskFileProvider>(serviceProvider, options);
        return builder;
    }
}