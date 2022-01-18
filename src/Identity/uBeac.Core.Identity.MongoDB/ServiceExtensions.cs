using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Core.Identity.MongoDB;
using uBeac.Identity;
using uBeac.Identity.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMongoDBUserRepository<TMongoDbContext, TUserKey, TUser>(this IServiceCollection services)
            where TMongoDbContext : class, IMongoDBContext
            where TUserKey : IEquatable<TUserKey>
            where TUser : User<TUserKey>
        {

            services.TryAddScoped<IUserRepository<TUserKey, TUser>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();

                if (dbContext == null)
                    throw new NullReferenceException("MongoDB Context is not registered for User.");

                return new MongoUserRepository<TUserKey, TUser>(dbContext);
            });

            return services;
        }

        public static IServiceCollection AddMongoDBUserRepository<TMongoDbContext, TUser>(this IServiceCollection services)
           where TMongoDbContext : class, IMongoDBContext
           where TUser : User
        {

            services.TryAddScoped<IUserRepository<TUser>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();

                if (dbContext == null)
                    throw new NullReferenceException("MongoDB Context is not registered for User.");

                return new MongoUserRepository<TUser>(dbContext);
            });

            return services;
        }

        public static IServiceCollection AddMongoDBRoleRepository<TMongoDbContext, TRoleKey, TRole>(this IServiceCollection services)
            where TMongoDbContext : class, IMongoDBContext
            where TRoleKey : IEquatable<TRoleKey>
            where TRole : Role<TRoleKey>
        {

            services.TryAddScoped<IRoleRepository<TRoleKey, TRole>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();

                if (dbContext == null)
                    throw new NullReferenceException("MongoDB Context is not registered for Role.");

                return new MongoRoleRepository<TRoleKey, TRole>(dbContext);
            });

            return services;
        }

        public static IServiceCollection AddMongoDBRoleRepository<TMongoDbContext, TRole>(this IServiceCollection services)
            where TMongoDbContext : class, IMongoDBContext
            where TRole : Role
        {

            services.TryAddScoped<IRoleRepository<TRole>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();

                if (dbContext == null)
                    throw new NullReferenceException("MongoDB Context is not registered for Role.");

                return new MongoRoleRepository<TRole>(dbContext);
            });

            return services;
        }

        public static IServiceCollection AddMongoDBUnitRepository<TMongoDbContext, TUnitKey, TUnit>(this IServiceCollection services)
            where TMongoDbContext : class, IMongoDBContext
            where TUnitKey : IEquatable<TUnitKey>
            where TUnit : Unit<TUnitKey>
        {

            services.TryAddScoped<IUnitRepository<TUnitKey, TUnit>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();

                if (dbContext == null)
                    throw new NullReferenceException("MongoDB Context is not registered for Role.");

                return new MongoUnitRepository<TUnitKey, TUnit>(dbContext);
            });

            return services;
        }

        public static IServiceCollection AddMongoDBUnitRepository<TMongoDbContext, TUnit>(this IServiceCollection services)
            where TMongoDbContext : class, IMongoDBContext
            where TUnit : Unit
        {

            services.TryAddScoped<IUnitRepository<TUnit>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();

                if (dbContext == null)
                    throw new NullReferenceException("MongoDB Context is not registered for Role.");

                return new MongoUnitRepository<TUnit>(dbContext);
            });

            return services;
        }
    }
}