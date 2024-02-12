using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Features.Authentication.Common;
using MediatR;

namespace ecommerce.Application.Features.Authentication.Queries.Login;
public sealed record LoginQuery(String Email, String Password) : IRequest<AuthenticationResponse>, IHasTransaction, IHasEvent;