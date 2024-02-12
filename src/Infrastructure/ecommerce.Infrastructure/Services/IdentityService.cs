using ecommerce.Application.Common.Extensions;
using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using ecommerce.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Infrastructure.Services;
internal sealed class IdentityService : IIdentityService {
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IDateTimeProvider dateTimeProvider;

    public IdentityService(UserManager<ApplicationUser> userManager, IDateTimeProvider dateTimeProvider) {
        this.userManager = userManager;
        this.dateTimeProvider = dateTimeProvider;
    }

    public async Task CreateAsync(UserId userId, String email, String password, CancellationToken cancellationToken) {
        ApplicationUser user = new() {
            UserId = userId,
            Email = email,
            UserName = email,
        };

        IdentityResult identityResult = await this.userManager.CreateAsync(user, password);

        if(identityResult.Succeeded.IsFalse()) {
            throw new Exception("Authentication failed");
        }
    }

    public async Task UpdateRefreshTokenAsync(UserId userId, String refreshToken) {
        ApplicationUser applicationUser = await this.userManager.Users.FirstOrDefaultAsync(user => user.UserId == userId)
            ?? throw new Exception("User not found");

        applicationUser.RefreshToken = refreshToken;
        applicationUser.RefreshTokenEndDate = this.dateTimeProvider.Now.AddDays(30);
        await this.userManager.UpdateAsync(applicationUser);
    }
}