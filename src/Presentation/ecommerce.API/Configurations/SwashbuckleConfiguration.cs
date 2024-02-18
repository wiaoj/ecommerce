using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ecommerce.API.Configurations;
public static class SwashbuckleConfiguration {
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services) {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ApiVersionSwaggerGenOptions>();
        services.AddSwaggerGen(
            options => {
                OpenApiSecurityScheme securityScheme = new() {
                    Name = "Authorization",
                    Description = "Enter a Bearer Token into the `Value` field to have it automatically prefixed with `Bearer ` and used as an `Authorization` header value for requests.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition("Bearer", securityScheme);
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement {
                        { securityScheme, Array.Empty<String>() }
                    });
            });
        return services;
    }
}