using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace uBeac;

public static class Extensions
{
    public static IServiceCollection AddEnumProvider(this IServiceCollection services)
    {
        services.AddSingleton(_ =>
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            return new EnumProvider(assembly.GetReferencedAssemblies());
        });

        return services;
    }
}