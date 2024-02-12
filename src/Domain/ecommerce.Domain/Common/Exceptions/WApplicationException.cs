namespace ecommerce.Domain.Common.Exceptions;
public abstract class WApplicationException(ErrorTypes errorType, string message) : Exception(message) {
    public string ErrorType => errorType.ToErrorName();
    protected WApplicationException() : this(ErrorTypes.Unknown, "Someting went wrong..") { }
}