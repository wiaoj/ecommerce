namespace ecommerce.Domain.Common.Exceptions;
public abstract class WApplicationException(ErrorTypes errorType, String message) : Exception(message) {
    public String ErrorType => errorType.ToErrorName();
    protected WApplicationException() : this(ErrorTypes.Unknown, "Someting went wrong..") { }
}