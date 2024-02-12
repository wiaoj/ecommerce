namespace ecommerce.Application.Features.Authentication.Exceptions;
public sealed class InvalidEmailException : ApplicationException {
    public InvalidEmailException() : base("User or password is incorrect.") { }
}