using ecommerce.Application.Common.Interfaces;

namespace ecommerce.Infrastructure.Services;
internal sealed class DateTimeProvider : IDateTimeProvider {
    public DateTime Now => DateTime.UtcNow;
}