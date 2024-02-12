using ecommerce.Domain.Aggregates.UserAggregate;

namespace ecommerce.Application.Common.Interfaces;
public interface IJwtTokenGenerator {
    String GenerateJwtToken(UserAggregate user);
}