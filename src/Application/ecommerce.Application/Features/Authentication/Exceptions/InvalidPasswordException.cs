namespace ecommerce.Application.Features.Authentication.Exceptions;
public sealed class InvalidPasswordException : ApplicationException {
    public InvalidPasswordException() : base("User or password is incorrect.") { }
}