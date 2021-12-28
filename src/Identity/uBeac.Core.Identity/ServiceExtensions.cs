using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using uBeac.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IdentityBuilder AddAuthenticationServices<TUserKey, TUser, TRoleKey, TRole>(this IServiceCollection services, Action<IdentityOptions> setupIdentityAction = default)
            where TUserKey : IEquatable<TUserKey>
            where TUser : User<TUserKey>, new()
            where TRoleKey : IEquatable<TRoleKey>
            where TRole : Role<TRoleKey>
        {

            services.AddScoped<IUserService<TUserKey, TUser>, UserService<TUserKey, TUser>>();
            services.AddScoped<IRoleService<TRoleKey, TRole>, RoleService<TRoleKey, TRole>>();
            //services.AddScoped<IUserRoleService<TKey, TUser, TRole>, UserRoleService<TKey, TUser, TRole>>();

            var builder = services.AddIdentityCore<TUser>(setupIdentityAction ?? (x => GetDefaultOptions()));

            builder
                .AddRoles<TRole>()
                .AddRoleStore<RoleStore<TRole, TRoleKey>>()
                .AddUserStore<UserStore<TUser, TUserKey>>()
                .AddUserManager<UserManager<TUser>>()
                .AddUserManager<UserManager<TUser>>()
                .AddRoleManager<RoleManager<TRole>>()
                .AddDefaultTokenProviders();

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
            var options = new IdentityOptions();

            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 1;
            options.Password.RequiredUniqueChars = 0;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

            return options;
        }
    }
}
