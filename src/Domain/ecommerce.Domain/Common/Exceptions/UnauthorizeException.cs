namespace ecommerce.Domain.Common.Exceptions;
public sealed class UnauthorizeException(String message) : WApplicationException(ExceptionCategories.UnAuthorization, message);