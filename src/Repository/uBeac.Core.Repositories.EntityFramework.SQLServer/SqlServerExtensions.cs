using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class SqlServerExtensions
{
    public static IServiceCollection AddSqlServerDatabase<T>(this IServiceCollection services, string connectionString, string assemblyName) where T : DbContext
    {
        services.AddEntityFrameworkSqlServer();
        services.AddDbContext<T>(options => options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(assemblyName)));
        return services;
    }
}