namespace ecommerce.Domain.Common.Exceptions;
public abstract class DomainValidationException(String message) : WApplicationException(ErrorTypes.DomainViolation, message);