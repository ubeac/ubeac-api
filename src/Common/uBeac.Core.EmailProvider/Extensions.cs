using Microsoft.Extensions.Configuration;
using uBeac;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddEmailProvider<TEmailProvider>(this IServiceCollection services, IConfiguration config)
        where TEmailProvider : class, IEmailProvider
    {
        services.Configure<EmailProviderOptions>(config.GetSection("Email"));
        services.AddScoped<IEmailProvider, TEmailProvider>();
        return services;
    }

    public static IServiceCollection AddEmailProvider(this IServiceCollection services, IConfiguration config)
        => AddEmailProvider<EmailProvider>(services, config);
}