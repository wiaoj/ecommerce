using ecommerce.Application.Common.Interfaces;

namespace ecommerce.Infrastructure.Services;
internal sealed class SystemDateTimeProvider : IDateTimeProvider {
    public DateTime UtcNow => DateTime.UtcNow;
}