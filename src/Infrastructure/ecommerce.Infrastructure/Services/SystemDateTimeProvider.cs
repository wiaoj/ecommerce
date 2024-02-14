using ecommerce.Domain.Common;

namespace ecommerce.Infrastructure.Services;
internal sealed class SystemDateTimeProvider : IDateTimeProvider {
    public DateTime UtcNow => DateTime.UtcNow;
}