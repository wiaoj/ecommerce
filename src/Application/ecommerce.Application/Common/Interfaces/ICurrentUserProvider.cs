namespace ecommerce.Application.Common.Interfaces;
public interface ICurrentUserProvider {
    String? UserId { get; }
    String? UserName { get; }
    String? UserIpAddress { get; }

    Task<Boolean> IsInRoleAsync(String role);
    Task<Boolean> AuthorizeAsync(String policy);
}