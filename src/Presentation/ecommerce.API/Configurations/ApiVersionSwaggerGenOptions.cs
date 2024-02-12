using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ecommerce.API.Configurations;
public class ApiVersionSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions> {
    private readonly IApiVersionDescriptionProvider provider;

    public ApiVersionSwaggerGenOptions(IApiVersionDescriptionProvider provider) {
        this.provider = provider;
    }

    public void Configure(SwaggerGenOptions options) {
        foreach(ApiVersionDescription? description in this.provider.ApiVersionDescriptions.OrderByDescending(o => o.ApiVersion)) {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description) {
        OpenApiInfo info = new() {
            Title = "ECommerce API",
            Version = description.ApiVersion.ToString()
        };

        if(description.IsDeprecated) {
            info.Description = "This API version has been deprecated.";
        }
        return info;
    }
}