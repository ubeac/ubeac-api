using Microsoft.Extensions.Configuration;
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
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));

            // supporting decimal values 
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));

            services.AddScoped(typeof(IEntityRepository<,>), typeof(MongoEntityRepository<,>));

            services.AddSingleton(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return new MongoDBOptions<TMongoDbContext>(configuration.GetConnectionString(connectionString));
            });

            services.AddSingleton<TMongoDbContext>();
            services.AddSingleton<IMongoDBContext, TMongoDbContext>();

            return services;

        }
    }
}
