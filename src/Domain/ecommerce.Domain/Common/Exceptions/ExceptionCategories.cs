namespace ecommerce.Domain.Common.Exceptions;
//TODO: refactor -- Ardalis.SmartEnum
public enum ExceptionCategories {
    Unknown,
    Validation,
    NotFound,
    Conflict,
    UnAuthorization, 
    BusinessRuleViolation
}

public static class ErrorTypeExtensions {
    public static string ToErrorName(this ExceptionCategories errorType) {
        return errorType.ToString();
    }
}