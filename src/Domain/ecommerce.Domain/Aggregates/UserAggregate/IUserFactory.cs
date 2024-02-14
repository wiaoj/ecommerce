using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Domain.Aggregates.UserAggregate;
public interface IUserFactory {
    UserAggregate Create(FullName fullName, Email email, PhoneNumber phoneNumber, Password password);
    Email CreateEmail(String email);
    PhoneNumber CreatePhoneNumber(String? phoneNumber);
    FullName CreateFullName(String firstName, String lastName);
    FullName CreateFullName(String firstName, IEnumerable<String>? middleNames, String lastName);
    Password CreatePassword(String password);
    RefreshToken CreateRefreshToken(String token,
                                    DateTime created,
                                    DateTime expires,
                                    String createdByIp);
}