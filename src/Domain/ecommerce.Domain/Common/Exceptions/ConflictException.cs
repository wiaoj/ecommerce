namespace ecommerce.Domain.Common.Exceptions;

public abstract class ConflictException(string message) : WApplicationException(ExceptionCategories.Conflict, message) {
    protected ConflictException() : this("Conflict") { }
}