namespace ecommerce.Application.Common.Interfaces;
public interface IHasDistributedCache {
    String CacheGroupKey { get; }
}