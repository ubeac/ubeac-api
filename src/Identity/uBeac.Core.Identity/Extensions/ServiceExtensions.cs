using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using uBeac;
using uBeac.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddUserRoleService<TUserRoleService, TUserKey, TUser>(this IServiceCollection services)
            where TUserKey : IEquatable<TUserKey>
            where TUser : User<TUserKey>
            where TUserRoleService : class, IUserRoleService<TUserKey, TUser>
        {
            services.TryAddScoped<IUserRoleService<TUserKey, TUser>, TUserRoleService>();
            return services;
        }

        public static IServiceCollection AddUserRoleService<TUserRoleService, TUser>(this IServiceCollection services)
           where TUser : User
           where TUserRoleService : class, IUserRoleService<TUser>
        {
            services.TryAddScoped<IUserRoleService<TUser>, TUserRoleService>();
            return services;
        }

        public static IServiceCollection AddUnitService<TUnitService, TUnitKey, TUnit>(this IServiceCollection services)
            where TUnitKey : IEquatable<TUnitKey>
            where TUnit : Unit<TUnitKey>
            where TUnitService : class, IUnitService<TUnitKey, TUnit>
        {
            services.TryAddScoped<IUnitService<TUnitKey, TUnit>, TUnitService>();
            return services;
        }

        public static IServiceCollection AddUnitService<TUnitService, TUnit>(this IServiceCollection services)
            where TUnit : Unit
            where TUnitService : class, IUnitService<TUnit>
        {
            services.TryAddScoped<IUnitService<TUnit>, TUnitService>();
            return services;
        }


        public static IServiceCollection AddUnitTypeService<TUnitTypeService, TUnitTypeKey, TUnitType>(this IServiceCollection services)
            where TUnitTypeKey : IEquatable<TUnitTypeKey>
            where TUnitType : UnitType<TUnitTypeKey>
            where TUnitTypeService : class, IUnitTypeService<TUnitTypeKey, TUnitType>
        {
            services.TryAddScoped<IUnitTypeService<TUnitTypeKey, TUnitType>, TUnitTypeService>();
            return services;
        }

        public static IServiceCollection AddUnitTypeService<TUnitTypeService, TUnitType>(this IServiceCollection services)
            where TUnitType : UnitType
            where TUnitTypeService : class, IUnitTypeService<TUnitType>
        {
            services.TryAddScoped<IUnitTypeService<TUnitType>, TUnitTypeService>();
            return services;
        }

        public static IServiceCollection AddUnitRoleService<TUnitRoleService, TUnitRoleKey, TUnitRole>(this IServiceCollection services)
            where TUnitRoleKey : IEquatable<TUnitRoleKey>
            where TUnitRole : UnitRole<TUnitRoleKey>
            where TUnitRoleService : class, IUnitRoleService<TUnitRoleKey, TUnitRole>
        {
            services.TryAddScoped<IUnitRoleService<TUnitRoleKey, TUnitRole>, TUnitRoleService>();
            return services;
        }

        public static IServiceCollection AddUnitRoleService<TUnitRoleService, TUnitRole>(this IServiceCollection services)
            where TUnitRole : UnitRole
            where TUnitRoleService : class, IUnitRoleService<TUnitRole>
        {
            services.TryAddScoped<IUnitRoleService<TUnitRole>, TUnitRoleService>();
            return services;
        }

        public static IServiceCollection AddUserService<TUserService, TUserKey, TUser>(this IServiceCollection services)
            where TUserKey : IEquatable<TUserKey>
            where TUser : User<TUserKey>
            where TUserService : class, IUserService<TUserKey, TUser>
        {
            services.TryAddScoped<IUserService<TUserKey, TUser>, TUserService>();
            return services;
        }

        public static IServiceCollection AddUserService<TUserService, TUser>(this IServiceCollection services)
           where TUser : User
           where TUserService : class, IUserService<TUser>
        {
            services.TryAddScoped<IUserService<TUser>, TUserService>();
            return services;
        }

        public static IServiceCollection AddRoleService<TRoleService, TRoleKey, TRole>(this IServiceCollection services)
            where TRoleKey : IEquatable<TRoleKey>
            where TRole : Role<TRoleKey>
            where TRoleService : class, IRoleService<TRoleKey, TRole>
        {
            services.TryAddScoped<IRoleService<TRoleKey, TRole>, TRoleService>();
            return services;
        }

        public static IServiceCollection AddRoleService<TRoleService, TRole>(this IServiceCollection services)
            where TRole : Role
            where TRoleService : class, IRoleService<TRole>
        {
            services.TryAddScoped<IRoleService<TRole>, TRoleService>();
            return services;
        }

        public static IdentityBuilder AddIdentityUser<TUser>(this IServiceCollection services, Action<IdentityOptions> configureIdentityOptions = default, Action<UserOptions<TUser>> configureOptions = default)
           where TUser : User
        {
            var builder = services.AddIdentityCore<TUser>(configureIdentityOptions ?? (x => GetDefaultOptions()));
            builder.Services.Configure(configureOptions);

            builder
                .AddUserStore<UserStore<TUser>>()
                .AddUserManager<UserManager<TUser>>()
                .AddDefaultTokenProviders();

            return builder;
        }

        public static IdentityBuilder AddIdentityUser<TUserKey, TUser>(this IServiceCollection services, Action<IdentityOptions> configureIdentityOptions = default, Action<UserOptions<TUserKey, TUser>> configureOptions = default)
            where TUserKey : IEquatable<TUserKey>
            where TUser : User<TUserKey>
        {
            var builder = services.AddIdentityCore<TUser>(configureIdentityOptions ?? (x => GetDefaultOptions()));
            builder.Services.Configure(configureOptions);

            builder
                .AddUserStore<UserStore<TUser, TUserKey>>()
                .AddUserManager<UserManager<TUser>>()
                .AddDefaultTokenProviders();

            return builder;
        }

        public static IdentityBuilder AddIdentityRole<TRoleKey, TRole>(this IdentityBuilder builder, Action<RoleOptions<TRoleKey, TRole>> configureOptions = default)
            where TRoleKey : IEquatable<TRoleKey>
            where TRole : Role<TRoleKey>
        {
            builder.Services.Configure(configureOptions);

            builder
                .AddRoles<TRole>()
                .AddRoleStore<RoleStore<TRole, TRoleKey>>()
                .AddRoleManager<RoleManager<TRole>>();

            return builder;
        }

        public static IdentityBuilder AddIdentityRole<TRole>(this IdentityBuilder builder, Action<RoleOptions<TRole>> configureOptions = default)
            where TRole : Role
        {
            builder.Services.Configure(configureOptions);

            builder
                .AddRoles<TRole>()
                .AddRoleStore<RoleStore<TRole>>()
                .AddRoleManager<RoleManager<TRole>>();

            return builder;
        }

        public static IdentityBuilder AddIdentityUnit<TUnitKey, TUnit>(this IdentityBuilder builder, Action<UnitOptions<TUnitKey, TUnit>> configureOptions = default)
            where TUnitKey : IEquatable<TUnitKey>
            where TUnit : Unit<TUnitKey>
        {
            builder.Services.Configure(configureOptions);

            return builder;
        }

        public static IdentityBuilder AddIdentityUnit<TUnit>(this IdentityBuilder builder, Action<UnitOptions<TUnit>> configureOptions = default)
            where TUnit : Unit
        {
            builder.Services.Configure(configureOptions);

            return builder;
        }

        public static IdentityBuilder AddIdentityUnitType<TUnitTypeKey, TUnitType>(this IdentityBuilder builder, Action<UnitTypeOptions<TUnitTypeKey, TUnitType>> configureOptions)
            where TUnitTypeKey : IEquatable<TUnitTypeKey>
            where TUnitType : UnitType<TUnitTypeKey>
        {
            builder.Services.Configure(configureOptions);

            return builder;
        }

        public static IdentityBuilder AddIdentityUnitType<TUnitType>(this IdentityBuilder builder, Action<UnitTypeOptions<TUnitType>> configureOptions)
            where TUnitType : UnitType
        {
            builder.Services.Configure(configureOptions);

            return builder;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtOptions options)
        {

            services.AddSingleton(options);

            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(options.Secret);
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // this will validate the 3rd part of the jwt token using the secret that we added in the appsettings and verify we have generated the jwt token
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    SaveSigninToken = true,
                    ValidAudience = options.Audience,
                    ValidIssuer = options.Issuer
                };
            });

            return services;
        }

        private static IdentityOptions GetDefaultOptions()
        {
            var options = new IdentityOptions
            {
                Password =
                {
                    // Password settings.
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequiredLength = 1,
                    RequiredUniqueChars = 0
                },
                Lockout =
                {
                    // Lockout settings.
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1),
                    MaxFailedAccessAttempts = 5,
                    AllowedForNewUsers = true
                },
                User =
                {
                    // User settings.
                    AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+",
                    RequireUniqueEmail = true
                }
            };

            return options;
        }
    }
}
