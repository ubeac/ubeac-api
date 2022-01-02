using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using uBeac.Repositories;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MongoDBServicesExtensions
    {
        public static IServiceCollection AddMongo<TMongoDbContext>(this IServiceCollection services, string connectionString)
           where TMongoDbContext : class, IMongoDBContext
        {

            // this will store Guids in MongoDB in string format to be readable in manual queries
            if (BsonSerializer.SerializerRegistry.GetSerializer<Guid>() == null)
                BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));

            // supporting decimal values 
            if (BsonSerializer.SerializerRegistry.GetSerializer<decimal>() == null)
                BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));

            if (BsonSerializer.SerializerRegistry.GetSerializer<decimal?>() == null)
                BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));

            services.TryAddSingleton(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return new MongoDBOptions<TMongoDbContext>(configuration.GetConnectionString(connectionString));
            });

            services.TryAddSingleton<TMongoDbContext>();
            services.TryAddSingleton<IMongoDBContext, TMongoDbContext>();

            return services;

        }

        public static IServiceCollection AddReporitory<TInterface, TImplementation>(this IServiceCollection services)
            where TInterface : IRepository
            where TImplementation : class, IRepository
        {
            services.TryAddScoped(typeof(TInterface), typeof(TImplementation));
            return services;

        }
        public static IServiceCollection AddReporitory(this IServiceCollection services, Type interfaceType, Type implementationType)
        {
            services.TryAddScoped(interfaceType, implementationType);
            return services;

        }
    }
}
