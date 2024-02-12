using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ecommerce.Persistance.Repositories;
internal sealed class UserRepository : Repository<UserAggregate, UserId, Guid>, IUserRepository {
    public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    public Task<UserAggregate?> FindByEmailAsync(Email email, CancellationToken cancellationToken) {
        return this.Query.FirstOrDefaultAsync(user => user.Email.Value == email.Value, cancellationToken);
    }

    public Task<UserAggregate?> FindByRefreshTokenAsync(String refreshToken, CancellationToken cancellationToken) {
        return this.Query.FirstOrDefaultAsync(predicate(refreshToken), cancellationToken);

        static Expression<Func<UserAggregate, Boolean>> predicate(String refreshToken) => 
            user => user.RefreshTokens.Any(x => x.Token == refreshToken);
    }
}