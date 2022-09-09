using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using uBeac;
using uBeac.Repositories;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class RepositoryExtensions
{
    public static IServiceCollection AddMongo<TMongoDbContext>(this IServiceCollection services, string connectionString)
        where TMongoDbContext : class, IMongoDBContext
    {
        services.TryAddSingleton(provider =>
        {
            var configuration = provider.GetService<IConfiguration>();
            var connString = configuration.GetConnectionString(connectionString);
            return new MongoDBOptions<TMongoDbContext>(connString);
        });

        services.TryAddSingleton<TMongoDbContext>();
        services.TryAddSingleton<IMongoDBContext, TMongoDbContext>();

        services.TryAddSingleton(provider =>
        {
            var appContextType = provider.CreateScope().ServiceProvider.GetRequiredService<IApplicationContext>().GetType();

            return new BsonSerializationOptions
            {
                Serializers = new Dictionary<Type, IBsonSerializer>
                {
                    { typeof(Guid), new GuidSerializer(GuidRepresentation.Standard) },
                    { typeof(decimal), new DecimalSerializer(BsonType.Decimal128) },
                    { typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)) },
                    { typeof(IApplicationContext), new AppContextBsonSerializer(appContextType) }
                },
                GuidRepresentationMode = GuidRepresentationMode.V3
            };
        });

        return services;
    }

    public static IServiceCollection AddRepository<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : IRepository
        where TImplementation : class, IRepository
    {
        services.TryAddScoped(typeof(TInterface), typeof(TImplementation));
        return services;

    }

    public static IServiceCollection AddRepository(this IServiceCollection services, Type interfaceType, Type implementationType)
    {
        services.TryAddScoped(interfaceType, implementationType);
        return services;
    }
}
