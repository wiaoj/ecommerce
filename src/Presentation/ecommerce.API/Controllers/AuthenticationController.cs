using Asp.Versioning;
using ecommerce.Application.Features.Authentication.Commands.Register;
using ecommerce.Application.Features.Authentication.Commands.RevokeRefreshToken;
using ecommerce.Application.Features.Authentication.Commands.RevokeRefreshTokens;
using ecommerce.Application.Features.Authentication.Common;
using ecommerce.Application.Features.Authentication.Queries.Login;
using ecommerce.Application.Features.Authentication.Queries.RefreshTokenLogin;
using ecommerce.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YamlDotNet.Core.Tokens;

namespace ecommerce.API.Controllers;
[Route("api/v{version:apiVersion}/authentication")]
[ApiVersion("1.0")]
public class AuthenticationController : BaseController {
    public AuthenticationController(ISender sender) : base(sender) { }

    public record RegisterRequest(String FirstName,
                                  String LastName,
                                  String Email,
                                  String Password);

    [HttpPost]
    [Route(nameof(Register))]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken) {
        RegisterCommand command = new(request.FirstName, null, request.LastName, request.Email, null, request.Password);
        AuthenticationResponse response = await this.Sender.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<IActionResult> Login(String email, String password, CancellationToken cancellationToken) {
        LoginQuery query = new(email, password);
        AuthenticationResponse response = await this.Sender.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [Route(nameof(RefreshTokenLogin))]
    public async Task<IActionResult> RefreshTokenLogin([FromHeader(Name = "X-RefreshToken")] String token,
                                                       CancellationToken cancellationToken) {
        RefreshTokenLoginQuery query = new(token);
        AuthenticationResponse response = await this.Sender.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [Route("revoke-all-refresh-tokens")]
    public async Task<IActionResult> RevokeRefreshTokens(CancellationToken cancellationToken) {
        RevokeRefreshTokensCommand query = new();
        await this.Sender.Send(query, cancellationToken);
        return Ok();
    }

    [HttpPost]
    [Route("revoke-refresh-token/{revokeToken}")]
    public async Task<IActionResult> RevokeRefreshToken([FromRoute] String revokeToken, CancellationToken cancellationToken) {
        RevokeRefreshTokenCommand query = new(revokeToken);
        await this.Sender.Send(query, cancellationToken);
        return Ok();
    }
}