using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace uBeac.Core.Repositories.EntityFramework.SQLServer
{
    public static class SqlServerExtensions
    {
        public static IServiceCollection AddSqlServerDatabase<T>(this IServiceCollection services) where T : DbContext
        {
            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<T>((serviceProvider, options) => options.UseSqlServer(typeof(T).Name));

            return services;
        }
    }
}