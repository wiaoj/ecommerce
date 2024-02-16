namespace ecommerce.Domain.Common.Exceptions;
public abstract class DomainValidationException(String message) : WApplicationException(ExceptionCategories.BusinessRuleViolation, message);