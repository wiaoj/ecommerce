using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ecommerce.API.Filters;
public class AuthorizeCheckOperationFilter : IOperationFilter {
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        if(!HasAuthorize(context)) {
            return;
        }
        operation.Security.Add(new OpenApiSecurityRequirement {
            [new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }] = Array.Empty<String>()
        });
    }

    private Boolean HasAuthorize(OperationFilterContext context) {
        return context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
|| context.MethodInfo.DeclaringType != null
            && context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
    }
}