using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Application.Common.Repositories;
public interface IUserRepository : IRepository<UserAggregate, UserId, Guid> {
    Task<UserAggregate?> FindByEmailAsync(Email email, CancellationToken cancellationToken);
    Task<UserAggregate?> FindByRefreshTokenAsync(String refreshToken, CancellationToken cancellationToken);
}