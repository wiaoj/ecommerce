using ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ecommerce.Infrastructure.Security;
internal sealed class CurrentUserProvider : ICurrentUserProvider {
    public String? UserId { get; }
    public String? UserName { get; }
    public String? UserIpAddress { get; }

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor) {
        ArgumentNullException.ThrowIfNull(httpContextAccessor);
        ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext);

        var claimsPrincipal = httpContextAccessor.HttpContext.User;
        this.UserId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        this.UserName = claimsPrincipal.FindFirstValue(ClaimTypes.Name);
        this.UserIpAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
    }

    public async Task<Boolean> AuthorizeAsync(String policy) {
        return await Task.FromResult(true);
    }

    public async Task<Boolean> IsInRoleAsync(String role) {
        return await Task.FromResult(true);
    }
}