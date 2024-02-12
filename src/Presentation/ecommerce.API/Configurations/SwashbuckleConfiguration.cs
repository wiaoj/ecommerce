using Asp.Versioning.ApiExplorer;
using ecommerce.API.Filters;
using ecommerce.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace ecommerce.API.Configurations;
public static class SwashbuckleConfiguration {
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration) {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ApiVersionSwaggerGenOptions>();
        services.AddSwaggerGen(
            options => {
                //options.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();
                options.SupportNonNullableReferenceTypes();
                options.CustomSchemaIds(x => x.FullName);

                String apiXmlFile = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

                if(File.Exists(apiXmlFile)) {
                    options.IncludeXmlComments(apiXmlFile);
                }

                String applicationXmlFile =
                    Path.Combine(AppContext.BaseDirectory, $"{typeof(DependencyInjection).Assembly.GetName().Name}.xml");

                if(File.Exists(applicationXmlFile)) {
                    options.IncludeXmlComments(applicationXmlFile);
                }

                options.OperationFilter<AuthorizeCheckOperationFilter>();

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

    public static void UseSwashbuckle(this IApplicationBuilder app, IConfiguration configuration) {
        app.UseSwagger();
        app.UseSwaggerUI(
            options => {
                options.RoutePrefix = "swagger";
                options.OAuthAppName("ECommerce API");
                options.EnableDeepLinking();
                options.DisplayOperationId();
                options.DefaultModelsExpandDepth(2);
                options.DefaultModelRendering(ModelRendering.Model);
                options.DocExpansion(DocExpansion.List);
                options.ShowExtensions();
                options.EnableFilter(String.Empty);
                AddSwaggerEndpoints(app, options);
                options.OAuthScopeSeparator(" ");
            });
    }

    private static void AddSwaggerEndpoints(IApplicationBuilder app, SwaggerUIOptions options) {
        IApiVersionDescriptionProvider provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach(ApiVersionDescription? description in provider.ApiVersionDescriptions.OrderByDescending(o => o.ApiVersion)) {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"{options.OAuthConfigObject.AppName} {description.GroupName}");
        }
    }
}

internal class RequireNonNullablePropertiesSchemaFilter : ISchemaFilter {
    public void Apply(OpenApiSchema model, SchemaFilterContext context) {
        IEnumerable<String> additionalRequiredProps = model.Properties
            .Where(x => !x.Value.Nullable && !model.Required.Contains(x.Key))
            .Select(x => x.Key);

        foreach(String? propKey in additionalRequiredProps) {
            model.Required.Add(propKey);
        }
    }
}