namespace ecommerce.Domain.Common.Exceptions;
public abstract class WApplicationException(ExceptionCategories errorType, String message) : Exception(message) {
    public String ErrorType => errorType.ToErrorName();
    protected WApplicationException() : this(ExceptionCategories.Unknown, "Someting went wrong..") { }
}