using uBeac;
using uBeac.FileManagement;

namespace Microsoft.Extensions.DependencyInjection;

public static class FileServiceBuilderExtensions
{
    public static IFileServiceBuilder<TKey, TEntity> SetValidExtensions<TKey, TEntity>(this IFileServiceBuilder<TKey, TEntity> builder, string[] validExtensions)
        where TKey : IEquatable<TKey>
        where TEntity : IFileEntity<TKey>
    {
        builder.AddValidator(new FileExtensionValidator(validExtensions));
        return builder;
    }
}