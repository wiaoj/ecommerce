using ecommerce.Domain.Aggregates.UserAggregate.Interfaces;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using System.Security.Cryptography;
using System.Text;

namespace ecommerce.Infrastructure.Security;
internal sealed class PasswordHasher : IPasswordHasher {
    const Int32 keySize = 64;
    const Int32 iterations = 350000;
    readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public String HashPassword(String password, out String salt) {
        Byte[] saltBytes = RandomNumberGenerator.GetBytes(keySize);
        salt = Convert.ToHexString(saltBytes);
        Byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            saltBytes,
            iterations,
            this.hashAlgorithm,
            keySize);
        return Convert.ToHexString(hash);
    }

    public Boolean VerifyPassword(Password password, String requestPassword) {
        Byte[] salt = Convert.FromHexString(password.Salt);
        Byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(requestPassword, salt, iterations, this.hashAlgorithm, keySize);
        Byte[] passwordBytes = Convert.FromHexString(password.HashedValue);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, passwordBytes);
    }
}