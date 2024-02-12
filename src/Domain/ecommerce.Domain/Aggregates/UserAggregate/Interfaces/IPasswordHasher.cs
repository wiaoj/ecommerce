using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Domain.Aggregates.UserAggregate.Interfaces;
public interface IPasswordHasher {
    String HashPassword(String password, out String salt);
    Boolean VerifyPassword(Password password, String requestPassword);
}