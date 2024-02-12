using Asp.Versioning;

namespace ecommerce.API.Configurations;
public static class ApiVersioningConfiguration {
    public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services) {
        services.AddApiVersioning(options => {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
        })
        .AddApiExplorer(options => {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        return services;
    }
}