using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Features.Authentication.Common;
using MediatR;

namespace ecommerce.Application.Features.Authentication.Queries.RefreshTokenLogin;
public sealed record RefreshTokenLoginQuery(String RefreshToken) : IRequest<AuthenticationResponse>, IHasTransaction, IHasEvent;