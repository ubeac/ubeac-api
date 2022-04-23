using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace uBeac.Enums;

public static class Extensions
{
    public static IServiceCollection AddAssemblyEnumsProvider(this IServiceCollection services)
    {
        services.AddSingleton<IAssemblyEnumsProvider>(_ =>
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null) throw new NullReferenceException(nameof(entryAssembly));

            var referencedAssemblies = entryAssembly.GetReferencedAssemblies().Select(Assembly.Load);
            var assemblies = new List<Assembly> { entryAssembly };
            assemblies.AddRange(referencedAssemblies);

            return new AssemblyEnumsProvider(assemblies);
        });

        return services;
    }
}