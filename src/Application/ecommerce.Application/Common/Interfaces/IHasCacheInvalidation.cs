namespace ecommerce.Application.Common.Interfaces;
public interface IHasCacheInvalidation {
    String CacheGroupKey { get; }
}