﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace uBeac.Core.Repositories.EntityFramework.Sqlite
{
    public static class SqliteExtensions
    {
        public static IServiceCollection AddSqliteDatabase<T>(this IServiceCollection services) where T : DbContext
        {
            services.AddEntityFrameworkSqlite();
            services.AddDbContext<T>((serviceProvider, options) =>
            options.UseSqlite(typeof(T).Name).UseInternalServiceProvider(serviceProvider));

            return services;
        }
    }
}