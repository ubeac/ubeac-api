using uBeac;
using uBeac.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddEmailTemplateService<TKey, TEmailTemplate, TService>(this IServiceCollection services)
        where TKey : IEquatable<TKey>
        where TEmailTemplate : EmailTemplateEntity<TKey>, new()
        where TService : class, IEmailTemplateService<TKey, TEmailTemplate>
    {
        services.AddScoped<IEmailTemplateService<TKey, TEmailTemplate>, TService>();
        return services;
    }

    public static IServiceCollection AddEmailTemplateService<TKey, TEmailTemplate>(this IServiceCollection services)
        where TKey : IEquatable<TKey>
        where TEmailTemplate : EmailTemplateEntity<TKey>, new()
    {
        services.AddScoped<IEmailTemplateService<TKey, TEmailTemplate>, EmailTemplateService<TKey, TEmailTemplate>>();
        return services;
    }

    public static IServiceCollection AddEmailTemplateService<TEmailTemplate>(this IServiceCollection services)
        where TEmailTemplate : EmailTemplateEntity, new()
    {
        services.AddScoped<IEmailTemplateService<TEmailTemplate>, EmailTemplateService<TEmailTemplate>>();
        return services;
    }

    public static IServiceCollection AddEmailTemplateService(this IServiceCollection services)
    {
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        return services;
    }
}