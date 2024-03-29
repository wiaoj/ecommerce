﻿using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;
using MediatR.Pipeline;

namespace ecommerce.Application.Common.Behaviours;
internal sealed class AuthorizationBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IAuthorizeRequest {
    private readonly ICurrentUserProvider currentUserProvider;

    public AuthorizationBehavior(ICurrentUserProvider currentUserProvider) {
        this.currentUserProvider = currentUserProvider;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken) {
        if(this.currentUserProvider.UserId.IsNullOrEmpty())
            throw new UnauthorizeException("Unauthorize");

        await Task.CompletedTask;
    }
}