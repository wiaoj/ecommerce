using ecommerce.Application.Features.Authentication.Commands.Register;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Application.Features.Authentication.MappingExtensions;
internal static class RegisterExtensions {
    public static UserAggregate FromRegisterCommand(this IUserFactory userFactory, RegisterCommand command) {
        FullName fullName = userFactory.CreateFullName(command.FirstName, command.LastName);
        Email email = userFactory.CreateEmail(command.Email);
        PhoneNumber phoneNumber = userFactory.CreatePhoneNumber(command.PhoneNumber);
        Password password = userFactory.CreatePassword(command.Password);
        return userFactory.Create(fullName, email, phoneNumber, password);
    }
}