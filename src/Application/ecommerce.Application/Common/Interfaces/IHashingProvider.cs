namespace ecommerce.Application.Common.Interfaces;
public interface IHashingProvider {
    String Generate(String input);
    String Generate<T>(T input);
}