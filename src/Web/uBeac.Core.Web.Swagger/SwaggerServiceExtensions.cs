using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class SwaggerServiceExtensions
{
    private static string _applicationName = string.Empty;
    private static string _applicationVersion = string.Empty;

    public static IServiceCollection AddCoreSwaggerWithJWT(this IServiceCollection services, string applicationName = "API", string applicationVersion = "v.1.0.0")
    {
        _applicationName = applicationName;
        _applicationVersion = applicationVersion;

        // Register the Swagger generator, defining 1 or more Swagger documents
        services.AddSwaggerGen(c =>
        {
            c.OrderActionsBy(x => x.RelativePath);

            c.SwaggerDoc("v1", new OpenApiInfo { Title = applicationName, Version = applicationVersion });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            var securityKeyScheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            };

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                    {
                        securityKeyScheme, new List<string>()
                    }
            });

        });

        //services.AddSwaggerGenNewtonsoftSupport();

        return services;

    }
    public static IApplicationBuilder UseCoreSwagger(this IApplicationBuilder app, string routePrefix = "doc")
    {
        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.DisplayRequestDuration();
            c.SwaggerEndpoint("/swagger/v1/swagger.json", _applicationName + ", Version " + _applicationVersion);
            c.RoutePrefix = routePrefix;
        });

        return app;
    }
}