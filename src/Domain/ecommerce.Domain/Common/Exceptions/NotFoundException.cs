namespace ecommerce.Domain.Common.Exceptions;
public abstract class NotFoundException(String message) : WApplicationException(ErrorTypes.NotFound, message);