using ecommerce.Domain.Common.Exceptions;
using WApplicationException = ecommerce.Domain.Common.Exceptions.WApplicationException;

namespace ecommerce.Application.Exceptions;
public sealed class ValidationException(IDictionary<String, String[]> errors) 
    : WApplicationException(ErrorTypes.Validation, "Input Validation Failed") {
    public IDictionary<String, String[]> Errors => errors;
}