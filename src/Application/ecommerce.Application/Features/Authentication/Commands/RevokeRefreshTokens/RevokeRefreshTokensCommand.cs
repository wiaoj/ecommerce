using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Authentication.Commands.RevokeRefreshTokens;
public sealed record RevokeRefreshTokensCommand : IRequest, IHasTransaction, IHasEvent;