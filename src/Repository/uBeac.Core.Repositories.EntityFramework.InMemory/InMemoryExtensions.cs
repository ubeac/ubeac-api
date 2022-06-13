using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class InMemoryExtensions
{
    public static IServiceCollection AddInMemoryDatabase<T>(this IServiceCollection services, string databaseName) where T : DbContext
    {
        services.AddEntityFrameworkInMemoryDatabase();
        services.AddDbContext<T>(options => options.UseInMemoryDatabase(databaseName));
        return services;
    }
}