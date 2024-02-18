using ecommerce.Domain.Common.Exceptions;

namespace ecommerce.Application.Features.Authentication.Exceptions;
public sealed class UserNotFoundException : NotFoundException {
    public UserNotFoundException() : base("") { }
}