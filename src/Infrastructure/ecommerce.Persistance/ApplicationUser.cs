using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace ecommerce.Persistance;
public sealed class ApplicationUser : IdentityUser<Guid> {
    public UserId UserId { get; set; }
    public String? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
}
public sealed class ApplicationUserRole : IdentityRole<Guid> { }