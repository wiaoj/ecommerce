using ecommerce.Application.Common.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ecommerce.Infrastructure.Security;
internal sealed class HashingProvider : IHashingProvider {
    public String Generate(String input) {
        return GenerateHashString(input);
    }

    public String Generate<T>(T input) {
        //String requestString = JsonSerializer.Serialize(input);
        String hashString = ComputeHash(input);
        return hashString;
    }

    private static String GenerateHashString(String input) {
        Byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        String hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        return hashString;
    }

    private static String ComputeHash<T>(T input) {
        String json = JsonSerializer.Serialize(input);
        Byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(json));
        return Convert.ToBase64String(hashBytes);
    }
}