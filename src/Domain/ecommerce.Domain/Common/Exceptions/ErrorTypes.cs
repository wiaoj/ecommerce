namespace ecommerce.Domain.Common.Exceptions;
//TODO: refactor -- Ardalis.SmartEnum
public enum ErrorTypes {
    Unknown,
    Validation,
    NotFound,
    Conflict,
    UnAuthorization, 
    DomainViolation
}

public static class ErrorTypeExtensions {
    public static string ToErrorName(this ErrorTypes errorType) {
        return errorType.ToString();
    }
}