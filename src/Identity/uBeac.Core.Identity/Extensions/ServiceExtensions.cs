using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;
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
            services.AddScoped<IUserRoleService<TUserKey, TUser>, TUserRoleService>();
            return services;
        }

        public static IServiceCollection AddUserRoleService<TUserRoleService, TUser>(this IServiceCollection services)
           where TUser : User
           where TUserRoleService : class, IUserRoleService<TUser>
        {
            services.AddScoped<IUserRoleService<TUser>, TUserRoleService>();
            return services;
        }

        public static IServiceCollection AddUnitService<TUnitService, TUnitKey, TUnit>(this IServiceCollection services)
            where TUnitKey : IEquatable<TUnitKey>
            where TUnit : Unit<TUnitKey>
            where TUnitService : class, IUnitService<TUnitKey, TUnit>
        {
            services.AddScoped<IUnitService<TUnitKey, TUnit>, TUnitService>();
            return services;
        }

        public static IServiceCollection AddUnitService<TUnitService, TUnit>(this IServiceCollection services)
            where TUnit : Unit
            where TUnitService : class, IUnitService<TUnit>
        {
            services.AddScoped<IUnitService<TUnit>, TUnitService>();
            return services;
        }


        public static IServiceCollection AddUnitTypeService<TUnitTypeService, TUnitTypeKey, TUnitType>(this IServiceCollection services)
            where TUnitTypeKey : IEquatable<TUnitTypeKey>
            where TUnitType : UnitType<TUnitTypeKey>
            where TUnitTypeService : class, IUnitTypeService<TUnitTypeKey, TUnitType>
        {
            services.AddScoped<IUnitTypeService<TUnitTypeKey, TUnitType>, TUnitTypeService>();
            return services;
        }

        public static IServiceCollection AddUnitTypeService<TUnitTypeService, TUnitType>(this IServiceCollection services)
            where TUnitType : UnitType
            where TUnitTypeService : class, IUnitTypeService<TUnitType>
        {
            services.AddScoped<IUnitTypeService<TUnitType>, TUnitTypeService>();
            return services;
        }

        public static IServiceCollection AddUnitRoleService<TUnitRoleService, TUnitRoleKey, TUnitRole>(this IServiceCollection services)
            where TUnitRoleKey : IEquatable<TUnitRoleKey>
            where TUnitRole : UnitRole<TUnitRoleKey>
            where TUnitRoleService : class, IUnitRoleService<TUnitRoleKey, TUnitRole>
        {
            services.AddScoped<IUnitRoleService<TUnitRoleKey, TUnitRole>, TUnitRoleService>();
            return services;
        }

        public static IServiceCollection AddUnitRoleService<TUnitRoleService, TUnitRole>(this IServiceCollection services)
            where TUnitRole : UnitRole
            where TUnitRoleService : class, IUnitRoleService<TUnitRole>
        {
            services.AddScoped<IUnitRoleService<TUnitRole>, TUnitRoleService>();
            return services;
        }

        public static IServiceCollection AddUserService<TUserService, TUserKey, TUser>(this IServiceCollection services)
            where TUserKey : IEquatable<TUserKey>
            where TUser : User<TUserKey>
            where TUserService : class, IUserService<TUserKey, TUser>
        {
            services.AddScoped<IUserService<TUserKey, TUser>, TUserService>();
            return services;
        }

        public static IServiceCollection AddUserService<TUserService, TUser>(this IServiceCollection services)
           where TUser : User
           where TUserService : class, IUserService<TUser>
        {
            services.AddScoped<IUserService<TUser>, TUserService>();
            return services;
        }

        public static IServiceCollection AddRoleService<TRoleService, TRoleKey, TRole>(this IServiceCollection services)
            where TRoleKey : IEquatable<TRoleKey>
            where TRole : Role<TRoleKey>
            where TRoleService : class, IRoleService<TRoleKey, TRole>
        {
            services.AddScoped<IRoleService<TRoleKey, TRole>, TRoleService>();
            return services;
        }

        public static IServiceCollection AddRoleService<TRoleService, TRole>(this IServiceCollection services)
            where TRole : Role
            where TRoleService : class, IRoleService<TRole>
        {
            services.AddScoped<IRoleService<TRole>, TRoleService>();
            return services;
        }

        public static IdentityBuilder AddIdentityUser<TUser>(this IServiceCollection services, Action<IdentityOptions> configureIdentityOptions = default, Action<UserOptions<TUser>> configureOptions = default)
           where TUser : User
        {
            var logger = services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<ILogger<User>>();
            logger.LogInformation("Start adding identity user");

            #region Configure AspNetIdentity

            services.AddDataProtection();
            var builder = services.AddIdentityCore<TUser>(configureIdentityOptions ?? (_ => GetDefaultOptions()));
            builder
                .AddUserStore<UserStore<TUser>>()
                .AddUserManager<UserManager<TUser>>()
                .AddDefaultTokenProviders();

            #endregion

            if (configureOptions is not null)
            {
                #region Register IOptions<UserOptions<,>>

                services.Configure(configureOptions);
                logger.LogInformation("Registered IOptions<UserOptions<,>>");

                #endregion

                #region Get UserOptions<,> from ServiceProvider

                var options = services.BuildServiceProvider().GetRequiredService<Options.IOptions<UserOptions<TUser>>>().Value;

                #endregion

                #region Register UserOptions<,> without IOptions

                services.AddSingleton<UserOptions<TUser>>(options);
                logger.LogInformation("Registered UserOptions<,> without IOptions");

                #endregion

                #region Create admin user and assign role

                if (options.AdminUser != null)
                {
                    var userManager = services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<UserManager<TUser>>();
                    if (userManager.Users.Any(user => user.UserName == options.AdminUser.UserName || user.NormalizedUserName == options.AdminUser.NormalizedUserName))
                    {
                        logger.LogInformation("Admin user is already exist");
                    }
                    else
                    {
                        logger.LogInformation("Start creating admin user");
                        if (options.AdminPassword == null) throw new ArgumentNullException(nameof(options.AdminPassword));
                        if (options.AdminRole == null) throw new ArgumentNullException(nameof(options.AdminRole));

                        userManager.CreateAsync(options.AdminUser, options.AdminPassword).Result.ThrowIfInvalid();
                        logger.LogInformation($"Created admin user - User name is {options.AdminUser.NormalizedUserName}");
                        userManager.AddToRoleAsync(options.AdminUser, options.AdminRole).Result.ThrowIfInvalid();
                        logger.LogInformation($"Assigned admin role to admin user - User name is {options.AdminUser.NormalizedUserName} - Role name is {options.AdminRole}");
                    }
                }
                else
                {
                    logger.LogWarning("Admin user is not defined");
                }

                #endregion
            }

            return builder;
        }

        public static IdentityBuilder AddIdentityUser<TUserKey, TUser>(this IServiceCollection services, Action<IdentityOptions> configureIdentityOptions = default, Action<UserOptions<TUserKey, TUser>> configureOptions = default)
            where TUserKey : IEquatable<TUserKey>
            where TUser : User<TUserKey>
        {
            var logger = services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<ILogger<User>>();
            logger.LogInformation("Start adding identity user");

            #region Configure AspNetIdentity

            services.AddDataProtection();
            var builder = services.AddIdentityCore<TUser>(configureIdentityOptions ?? (_ => GetDefaultOptions()));
            builder
                .AddUserStore<UserStore<TUser, TUserKey>>()
                .AddUserManager<UserManager<TUser>>()
                .AddDefaultTokenProviders();

            #endregion

            if (configureOptions is not null)
            {
                #region Register IOptions<UserOptions<,>>

                services.Configure(configureOptions);
                logger.LogInformation("Registered IOptions<UserOptions<,>>");

                #endregion

                #region Get UserOptions<,> from ServiceProvider

                var options = services.BuildServiceProvider().GetRequiredService<Options.IOptions<UserOptions<TUserKey, TUser>>>().Value;

                #endregion

                #region Register UserOptions<,> without IOptions

                services.AddSingleton<UserOptions<TUserKey, TUser>>(options);
                logger.LogInformation("Registered UserOptions<,> without IOptions");

                #endregion

                #region Create admin user and assign role

                if (options.AdminUser != null)
                {
                    var userManager = services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<UserManager<TUser>>();
                    if (userManager.Users.Any(user => user.UserName == options.AdminUser.UserName || user.NormalizedUserName == options.AdminUser.NormalizedUserName))
                    {
                        logger.LogInformation("Admin user is already exist");
                    }
                    else
                    {
                        logger.LogInformation("Start creating admin user");
                        if (options.AdminPassword == null) throw new ArgumentNullException(nameof(options.AdminPassword));
                        if (options.AdminRole == null) throw new ArgumentNullException(nameof(options.AdminRole));

                        userManager.CreateAsync(options.AdminUser, options.AdminPassword).Result.ThrowIfInvalid();
                        logger.LogInformation($"Created admin user - User name is {options.AdminUser.NormalizedUserName}");
                        userManager.AddToRoleAsync(options.AdminUser, options.AdminRole).Result.ThrowIfInvalid();
                        logger.LogInformation($"Assigned admin role to admin user - User name is {options.AdminUser.NormalizedUserName} - Role name is {options.AdminRole}");
                    }
                }
                else
                {
                    logger.LogWarning("Admin user is not defined");
                }

                #endregion
            }

            return builder;
        }

        public static IdentityBuilder AddIdentityRole<TRoleKey, TRole>(this IdentityBuilder builder, Action<RoleOptions<TRoleKey, TRole>> configureOptions = default)
            where TRoleKey : IEquatable<TRoleKey>
            where TRole : Role<TRoleKey>
        {
            var logger = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<ILogger<Role>>();
            logger.LogInformation("Start adding identity role");

            #region Configure AspNetIdentity

            builder
                .AddRoles<TRole>()
                .AddRoleStore<RoleStore<TRole, TRoleKey>>()
                .AddRoleManager<RoleManager<TRole>>();

            #endregion

            if (configureOptions is not null)
            {
                #region Register IOptions<RoleOptions<,>>

                builder.Services.Configure(configureOptions);
                logger.LogInformation("Registered IOptions<RoleOptions<,>>");

                #endregion

                #region Get RoleOptions<,> from ServiceProvider

                var options = builder.Services.BuildServiceProvider().GetRequiredService<Options.IOptions<RoleOptions<TRoleKey, TRole>>>().Value;

                #endregion

                #region Register RoleOptions<,> without IOptions

                builder.Services.AddSingleton<RoleOptions<TRoleKey, TRole>>(options);
                logger.LogInformation("Registered RoleOptions<,> without IOptions");

                #endregion

                #region Insert default roles

                // Get RoleService<,> from ServiceProvider
                var roleService = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<IRoleService<TRoleKey, TRole>>();
                if (options.DefaultValues?.Any() == true)
                {
                    // Insert default roles
                    foreach (var role in options.DefaultValues)
                    {
                        if (roleService.Exists(role.Name).Result) logger.LogInformation($"Role {role.Name} is already exist");
                        else
                        {
                            roleService.Insert(role).Wait();
                            logger.LogInformation($"Created {role.Name} role");
                        }
                    }
                }
                else
                {
                    logger.LogWarning("Default roles is not defined");
                }

                #endregion
            }

            return builder;
        }

        public static IdentityBuilder AddIdentityRole<TRole>(this IdentityBuilder builder, Action<RoleOptions<TRole>> configureOptions = default)
            where TRole : Role
        {
            var logger = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<ILogger<Role>>();
            logger.LogInformation("Start adding identity role");

            #region Configure AspNetIdentity

            builder
                .AddRoles<TRole>()
                .AddRoleStore<RoleStore<TRole>>()
                .AddRoleManager<RoleManager<TRole>>();

            #endregion

            if (configureOptions is not null)
            {
                #region Register IOptions<RoleOptions<,>>

                builder.Services.Configure(configureOptions);
                logger.LogInformation("Registered IOptions<RoleOptions<,>>");

                #endregion

                #region Get RoleOptions<,> from ServiceProvider

                var options = builder.Services.BuildServiceProvider().GetRequiredService<Options.IOptions<RoleOptions<TRole>>>().Value;

                #endregion

                #region Register RoleOptions<,> without IOptions

                builder.Services.AddSingleton<RoleOptions<TRole>>(options);
                logger.LogInformation("Registered RoleOptions<,> without IOptions");

                #endregion

                #region Insert default roles

                // Get RoleService<,> from ServiceProvider
                var roleService = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<IRoleService<TRole>>();
                if (options.DefaultValues?.Any() == true)
                {
                    // Insert default roles
                    foreach (var role in options.DefaultValues)
                    {
                        if (roleService.Exists(role.Name).Result) logger.LogInformation($"Role {role.Name} is already exist");
                        else
                        {
                            roleService.Insert(role).Wait();
                            logger.LogInformation($"Created {role.Name} role");
                        }
                    }
                }
                else
                {
                    logger.LogWarning("Default roles is not defined");
                }

                #endregion
            }

            return builder;
        }

        public static IdentityBuilder AddIdentityUnit<TUnitKey, TUnit>(this IdentityBuilder builder, Action<UnitOptions<TUnitKey, TUnit>> configureOptions = default)
            where TUnitKey : IEquatable<TUnitKey>
            where TUnit : Unit<TUnitKey>
        {
            var logger = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<ILogger<Unit>>();
            logger.LogInformation("Start adding identity unit");

            if (configureOptions is not null)
            {
                #region Register IOptions<UnitOptions<,>>

                builder.Services.Configure(configureOptions);
                logger.LogInformation("Registered IOptions<UnitOptions<,>>");

                #endregion

                #region Get UnitOptions<,> from ServiceProvider

                var options = builder.Services.BuildServiceProvider().GetRequiredService<Options.IOptions<UnitOptions<TUnitKey, TUnit>>>().Value;
                logger.LogInformation("Registered UnitOptions<,> without IOptions");

                #endregion

                #region Register UnitOptions<,> without IOptions

                builder.Services.AddSingleton<UnitOptions<TUnitKey, TUnit>>(options);

                #endregion

                #region Insert default units

                // Get UnitService<,> from ServiceProvider
                var unitService = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<IUnitService<TUnitKey, TUnit>>();
                if (options.DefaultValues?.Any() == true)
                {
                    // Insert default units
                    foreach (var unit in options.DefaultValues)
                    {
                        if (unitService.Exists(unit.Code, unit.Type).Result) logger.LogInformation($"{unit.Name} unit (type: {unit.Type}, code: {unit.Code}) is already exist");
                        else
                        {
                            unitService.Insert(unit).Wait();
                            logger.LogInformation($"Created {unit.Name} unit (type: {unit.Type}, code: {unit.Code})");
                        }
                    }
                }
                else
                {
                    logger.LogWarning("Default units is not defined");
                }

                #endregion
            }

            return builder;
        }

        public static IdentityBuilder AddIdentityUnit<TUnit>(this IdentityBuilder builder, Action<UnitOptions<TUnit>> configureOptions = default)
            where TUnit : Unit
        {
            var logger = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<ILogger<Unit>>();
            logger.LogInformation("Start adding identity unit");

            if (configureOptions is not null)
            {
                #region Register IOptions<UnitOptions<,>>

                builder.Services.Configure(configureOptions);
                logger.LogInformation("Registered IOptions<UnitOptions<,>>");

                #endregion

                #region Get UnitOptions<,> from ServiceProvider

                var options = builder.Services.BuildServiceProvider().GetRequiredService<Options.IOptions<UnitOptions<TUnit>>>().Value;
                logger.LogInformation("Registered UnitOptions<,> without IOptions");

                #endregion

                #region Register UnitOptions<,> without IOptions

                builder.Services.AddSingleton<UnitOptions<TUnit>>(options);

                #endregion

                #region Insert default units

                // Get UnitService<,> from ServiceProvider
                var unitService = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<IUnitService<TUnit>>();
                if (options.DefaultValues?.Any() == true)
                {
                    // Insert default units
                    foreach (var unit in options.DefaultValues)
                    {
                        if (unitService.Exists(unit.Code, unit.Type).Result) logger.LogInformation($"{unit.Name} unit (type: {unit.Type}, code: {unit.Code}) is already exist");
                        else
                        {
                            unitService.Insert(unit).Wait();
                            logger.LogInformation($"Created {unit.Name} unit (type: {unit.Type}, code: {unit.Code})");
                        }
                    }
                }
                else
                {
                    logger.LogWarning("Default units is not defined");
                }

                #endregion
            }
            
            return builder;
        }

        public static IdentityBuilder AddIdentityUnitType<TUnitTypeKey, TUnitType>(this IdentityBuilder builder, Action<UnitTypeOptions<TUnitTypeKey, TUnitType>> configureOptions = default)
            where TUnitTypeKey : IEquatable<TUnitTypeKey>
            where TUnitType : UnitType<TUnitTypeKey>
        {
            var logger = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<ILogger<UnitType>>();
            logger.LogInformation("Start adding identity unit type");

            if (configureOptions is not null)
            {
                #region Register IOptions<UnitTypeOptions<,>>

                builder.Services.Configure(configureOptions);
                logger.LogInformation("Registered IOptions<UnitTypeOptions<,>>");

                #endregion

                #region Get UnitTypeOptions<,> from ServiceProvider

                var options = builder.Services.BuildServiceProvider().GetRequiredService<Options.IOptions<UnitTypeOptions<TUnitTypeKey, TUnitType>>>().Value;

                #endregion

                #region Register UnitTypeOptions<,> without IOptions

                builder.Services.AddSingleton<UnitTypeOptions<TUnitTypeKey, TUnitType>>(options);
                logger.LogInformation("Registered UnitTypeOptions<,> without IOptions");

                #endregion

                #region Insert default unit types

                // Get UnitTypeService<,> from ServiceProvider
                var unitTypeService = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<IUnitTypeService<TUnitTypeKey, TUnitType>>();
                if (options.DefaultValues?.Any() == true)
                {
                    // Insert default units
                    foreach (var unitType in options.DefaultValues)
                    {
                        if (unitTypeService.Exists(unitType.Code).Result) logger.LogInformation($"{unitType.Name} unit type (code: {unitType.Code}) is already exist");
                        else
                        {
                            unitTypeService.Insert(unitType).Wait();
                            logger.LogInformation($"Created {unitType.Name} unit type (code: {unitType.Code})");
                        }
                    }
                }
                else
                {
                    logger.LogWarning("Default unit types is not defined");
                }

                #endregion
            }

            return builder;
        }

        public static IdentityBuilder AddIdentityUnitType<TUnitType>(this IdentityBuilder builder, Action<UnitTypeOptions<TUnitType>> configureOptions = default)
            where TUnitType : UnitType
        {
            var logger = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<ILogger<UnitType>>();
            logger.LogInformation("Start adding identity unit type");

            if (configureOptions is not null)
            {
                #region Register IOptions<UnitTypeOptions<,>>

                builder.Services.Configure(configureOptions);
                logger.LogInformation("Registered IOptions<UnitTypeOptions<,>>");

                #endregion

                #region Get UnitTypeOptions<,> from ServiceProvider

                var options = builder.Services.BuildServiceProvider().GetRequiredService<Options.IOptions<UnitTypeOptions<TUnitType>>>().Value;

                #endregion

                #region Register UnitTypeOptions<,> without IOptions

                builder.Services.AddSingleton<UnitTypeOptions<TUnitType>>(options);
                logger.LogInformation("Registered UnitTypeOptions<,> without IOptions");

                #endregion

                #region Insert default unit types

                // Get UnitTypeService<,> from ServiceProvider
                var unitTypeService = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<IUnitTypeService<TUnitType>>();
                if (options.DefaultValues?.Any() == true)
                {
                    // Insert default units
                    foreach (var unitType in options.DefaultValues)
                    {
                        if (unitTypeService.Exists(unitType.Code).Result) logger.LogInformation($"{unitType.Name} unit type (code: {unitType.Code}) is already exist");
                        else
                        {
                            unitTypeService.Insert(unitType).Wait();
                            logger.LogInformation($"Created {unitType.Name} unit type (code: {unitType.Code})");
                        }
                    }
                }
                else
                {
                    logger.LogWarning("Default unit types is not defined");
                }

                #endregion
            }

            return builder;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtOptions options)
        {
            services.AddSingleton(options);
            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

            services.AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
                    // Password settings
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequiredLength = 1,
                    RequiredUniqueChars = 0
                },
                Lockout =
                {
                    // Lockout settings
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1),
                    MaxFailedAccessAttempts = 5,
                    AllowedForNewUsers = true
                },
                User =
                {
                    // User settings
                    AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+",
                    RequireUniqueEmail = true
                }
            };

            return options;
        }
    }
}
