using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using uBeac;
using uBeac.Repositories;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class MongoDBServicesExtensions
{
    public static IServiceCollection AddMongo<TMongoDbContext>(this IServiceCollection services, string connectionString, bool dropExistDatabase = false)
       where TMongoDbContext : class, IMongoDBContext
    {
        services.TryAddSingleton(provider =>
        {
            var configuration = provider.GetService<IConfiguration>();
            var connString = configuration.GetConnectionString(connectionString);
            return typeof(TMongoDbContext) == typeof(MongoDBContext)
                ? new MongoDBOptions(connString, dropExistDatabase)
                : new MongoDBOptions<TMongoDbContext>(connString, dropExistDatabase);
        });

        services.TryAddSingleton<TMongoDbContext>();
        services.TryAddSingleton<IMongoDBContext, TMongoDbContext>();

        return services;
    }

    public static IServiceCollection AddDefaultBsonSerializers(this IServiceCollection services)
    {
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;

        try
        {
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            BsonSerializer.RegisterSerializer(typeof(IApplicationContext), new AppContextBsonSerializer());
        }
        catch (BsonSerializationException)
        {
        }

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
