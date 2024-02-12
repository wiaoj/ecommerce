namespace ecommerce.Domain.Common.Exceptions;
public class UnauthorizeException(String message) : WApplicationException(ErrorTypes.UnAuthorization, message);