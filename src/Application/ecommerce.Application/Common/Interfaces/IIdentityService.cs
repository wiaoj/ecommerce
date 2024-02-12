using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Application.Common.Interfaces;
public interface IIdentityService {
    Task CreateAsync(UserId userId, String email, String password, CancellationToken cancellationToken);
    Task UpdateRefreshTokenAsync(UserId userId, String refreshToken);
}