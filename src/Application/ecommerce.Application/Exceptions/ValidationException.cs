using ecommerce.Domain.Common.Exceptions;

namespace ecommerce.Application.Exceptions;
public sealed class ValidationException(IDictionary<String, String[]> errors) 
    : WApplicationException(ExceptionCategories.Validation, "Input Validation Failed") {
    public IDictionary<String, String[]> Errors => errors;
}