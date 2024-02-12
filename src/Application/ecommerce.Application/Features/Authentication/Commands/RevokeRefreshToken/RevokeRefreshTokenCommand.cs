using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Authentication.Commands.RevokeRefreshToken;
public sealed record RevokeRefreshTokenCommand(String RevokeToken) : IRequest, IHasTransaction, IHasEvent;