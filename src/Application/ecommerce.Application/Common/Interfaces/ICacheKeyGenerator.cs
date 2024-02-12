namespace ecommerce.Application.Common.Interfaces;
public interface ICacheKeyGenerator {
    String Generate<T>(T request);
}