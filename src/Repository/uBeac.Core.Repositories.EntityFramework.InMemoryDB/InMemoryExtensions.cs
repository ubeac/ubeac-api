using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace uBeac.Core.Repositories.EntityFramework.InMemoryDB
{
    public static class InMemoryExtensions
    {
        public static IServiceCollection AddInMemoryDatabase<T>(this IServiceCollection services) where T : DbContext
        {
            services.AddDbContext<T>(opts => opts.UseInMemoryDatabase(databaseName: typeof(T).Name));
            return services;
        }
    }
}