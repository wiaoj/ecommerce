namespace ecommerce.Domain.Common.Exceptions;

public abstract class ConflictException(string message) : WApplicationException(ErrorTypes.Conflict, message) {
    protected ConflictException() : this("Conflict") { }
}