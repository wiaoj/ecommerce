namespace ecommerce.Application.Features.Authentication.Common;
public sealed record AuthenticationResponse(String Token, String RefreshToken);