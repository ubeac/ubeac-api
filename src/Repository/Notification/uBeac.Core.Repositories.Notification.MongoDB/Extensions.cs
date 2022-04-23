using uBeac;
using uBeac.Repositories;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddMongoEmailTemplateRepository<TKey, TEmailTemplate, TContext>(this IServiceCollection services)
        where TKey : IEquatable<TKey>
        where TEmailTemplate : EmailTemplateEntity<TKey>
        where TContext : IMongoDBContext
    {
        services.AddScoped<IEmailTemplateRepository<TKey, TEmailTemplate>, MongoEmailTemplateRepository<TKey, TEmailTemplate, TContext>>();
        return services;
    }

    public static IServiceCollection AddMongoEmailTemplateRepository<TEmailTemplate, TContext>(this IServiceCollection services)
        where TEmailTemplate : EmailTemplateEntity
        where TContext : IMongoDBContext
    {
        services.AddScoped<IEmailTemplateRepository<TEmailTemplate>, MongoEmailTemplateRepository<TEmailTemplate, TContext>>();
        return services;
    }

    public static IServiceCollection AddMongoEmailTemplateRepository<TContext>(this IServiceCollection services)
        where TContext : IMongoDBContext
    {
        services.AddScoped<IEmailTemplateRepository, MongoEmailTemplateRepository<TContext>>();
        return services;
    }

    public static IServiceCollection AddMongoEmailTemplateRepository(this IServiceCollection services)
    {
        services.AddScoped<IEmailTemplateRepository, MongoEmailTemplateRepository<MongoDBContext>>();
        return services;
    }
}