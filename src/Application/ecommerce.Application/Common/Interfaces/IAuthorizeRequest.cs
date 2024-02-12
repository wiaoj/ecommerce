namespace ecommerce.Application.Common.Interfaces;
public interface IAuthorizeRequest {
    IEnumerable<String> Roles { get; }
    IEnumerable<String> Permissions { get; }
}