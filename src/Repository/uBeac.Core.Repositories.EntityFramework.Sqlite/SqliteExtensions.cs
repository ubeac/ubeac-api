using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class SqliteExtensions
{
    public static IServiceCollection AddSqliteDatabase<T>(this IServiceCollection services, string connectionString) where T : DbContext
    {
        services.AddEntityFrameworkSqlite();
        services.AddDbContext<T>(options => options.UseSqlite(connectionString));
        return services;
    }
}