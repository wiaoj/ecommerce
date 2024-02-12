using ecommerce.Application.Common.Constants;
using ecommerce.Domain.Common.Exceptions;

namespace ecommerce.Application.Features.Authentication.Exceptions;
public sealed class UserNotFoundException : NotFoundException {
    public UserNotFoundException() : base(Errors.User.NotFound) { }
    public UserNotFoundException(Guid id) : base(Errors.User.NotFoundById(id)) { }
}