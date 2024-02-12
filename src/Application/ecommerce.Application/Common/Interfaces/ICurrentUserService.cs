namespace ecommerce.Application.Common.Interfaces;
public interface ICurrentUserService {
    String UserId { get; }
    String UserName { get; }
    String UserIpAddress { get; }

    Task<Boolean> IsInRoleAsync(String role);
    Task<Boolean> AuthorizeAsync(String policy);
}