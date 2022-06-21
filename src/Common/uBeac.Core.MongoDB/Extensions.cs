using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace uBeac.Repositories.MongoDB;

public static class MongoDBExtensions
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
}