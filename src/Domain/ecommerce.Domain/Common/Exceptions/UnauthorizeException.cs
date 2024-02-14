namespace ecommerce.Domain.Common.Exceptions;
public sealed class UnauthorizeException(String message) : WApplicationException(ErrorTypes.UnAuthorization, message);