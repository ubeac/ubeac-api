using uBeac.Identity;
using uBeac.Identity.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMongoDBIdentity<TMongoDbContext, TUserKey, TUser, TRoleKey, TRole>(this IServiceCollection services)
            where TMongoDbContext : class, IMongoDBContext
            where TUserKey : IEquatable<TUserKey>
            where TUser : User<TUserKey>
            where TRoleKey : IEquatable<TRoleKey>
            where TRole : Role<TRoleKey>
        {

            services.AddScoped<IUserRepository<TUserKey, TUser>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();
                
                if (dbContext == null)
                    throw new NullReferenceException("MongoDB Context is not registered.");

                return new MongoUserRepository<TUserKey, TUser>(dbContext);
            });

            services.AddScoped<IRoleRepository<TRoleKey, TRole>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();

                if (dbContext == null)
                    throw new NullReferenceException("MongoDB Context is not registered.");

                return new MongoRoleRepository<TRoleKey, TRole>(dbContext);
            });

            return services;
        }

    }
}