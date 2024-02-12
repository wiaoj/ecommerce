using ecommerce.Application.Features.Authentication.Commands.Register;

namespace ecommerce.Application.UnitTests.Features.Authentication.TestUtils;
public static class RegisterCommandUtils {
    public static RegisterCommand CreateCommand() {
        return new(User.FirstName,
                   User.MiddleNames,
                   User.LastName,
                   User.Email,
                   User.PhoneNumber,
                   User.Password);
    }
}