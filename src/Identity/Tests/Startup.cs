using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using uBeac.Identity;
using uBeac.Repositories.MongoDB;
using uBeac.Web.Middlewares;

namespace Identity
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public IHostEnvironment Environment { get; }

        public Startup(IHostEnvironment env)
        {
            Environment = env;
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonConfig(env);
            Configuration = configBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddSingleton<IConfiguration>(Configuration);
            
            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddMongo<MongoDBContext>("DefaultConnection");

            // Adding repositories
            services.AddMongoDBUserRepository<MongoDBContext, User>();
            services.AddMongoDBRoleRepository<MongoDBContext, Role>();
            services.AddMongoDBUnitRepository<MongoDBContext, Unit>();

            // Adding services
            services.AddUserService<UserService<User>, User>();
            services.AddRoleService<RoleService<Role>, Role>();
            services.AddUserRoleService<UserRoleService<User>, User>();

            // Adding Core Identity
            services
                .AddIdentityUser<User>()
                .AddIdentityRole<Role>();

            services.AddJwtAuthentication(Configuration.GetInstance<JwtOptions>("Jwt"));
        }
        public void Configure(IApplicationBuilder app)
        {

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}