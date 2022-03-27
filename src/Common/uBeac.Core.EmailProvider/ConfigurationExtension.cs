using uBeac;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigurationExtension
{
    public static IServiceCollection AddEmailProvider<TEmailProvider>(this IServiceCollection services, EmailProviderOptions options)
        where TEmailProvider : class, IEmailProvider
    {
        services.AddSingleton(options);
        services.AddScoped<IEmailProvider, TEmailProvider>();
        return services;
    }

    public static IServiceCollection AddEmailProvider(this IServiceCollection services, EmailProviderOptions options)
        => AddEmailProvider<EmailProvider>(services, options);
}