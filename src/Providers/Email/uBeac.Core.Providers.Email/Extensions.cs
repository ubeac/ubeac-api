using Microsoft.Extensions.Configuration;
using uBeac.Providers.Email;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddEmailProvider(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("Smtp").Get<SmtpSettings>();

        services.AddSingleton(options);
        services.AddScoped<IEmailProvider, EmailProvider>();

        return services;
    }
}