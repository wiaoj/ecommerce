﻿using ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ecommerce.Infrastructure.Services;
internal sealed class CurrentUserService : ICurrentUserService {
    private const String UNKNOWN = "UNKNOWN";
    private readonly ClaimsPrincipal? claimsPrincipal;
    public String UserId => this.claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier) ?? UNKNOWN;
    public String UserName => this.claimsPrincipal?.FindFirstValue(ClaimTypes.Name) ?? UNKNOWN;
    public String UserIpAddress { get; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor) {
        this.claimsPrincipal = httpContextAccessor?.HttpContext?.User;
        this.UserIpAddress = httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? UNKNOWN;
    }

    public async Task<Boolean> AuthorizeAsync(String policy) {
        return await Task.FromResult(true);
    }

    public async Task<Boolean> IsInRoleAsync(String role) {
        return await Task.FromResult(true);
    }
}