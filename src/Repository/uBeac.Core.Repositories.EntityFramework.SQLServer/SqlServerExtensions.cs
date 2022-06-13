using Microsoft.EntityFrameworkCore;
using uBeac.Repositories.EntityFramework;

namespace Microsoft.Extensions.DependencyInjection;

public static class SqlServerExtensions
{
    public static IServiceCollection AddSqlServerDatabase<T>(this IServiceCollection services, string connectionString) where T : EFDbContext
    {
        services.AddEntityFrameworkSqlServer();
        services.AddDbContext<T>(options => options.UseSqlServer(connectionString));
        return services;
    }
}