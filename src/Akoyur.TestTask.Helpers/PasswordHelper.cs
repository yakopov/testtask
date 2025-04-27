using System.Security.Cryptography;

namespace Akoyur.TestTask.Helpers;

/// <summary>
/// Helper class for password hashing and verification.
/// </summary>
public static class PasswordHelper
{
    /// <summary>
    /// Salt size in bytes (128 bits).
    /// </summary>
    private const int SaltSize = 16; // 128 bits

    /// <summary>
    /// Key size in bytes (256 bits).
    /// </summary>
    private const int KeySize = 32;  // 256 bits

    /// <summary>
    /// The number of iterations for PBKDF2 (recommended by OWASP).
    /// </summary>
    private const int Iterations = 100_000; // Recommended by OWASP

    /// <summary>
    /// The hashing algorithm to be used (SHA-256).
    /// </summary>
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

    /// <summary>
    /// Creates a password hash using PBKDF2 with a random salt.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>A hashed password string including the iterations, salt, and hash.</returns>
    public static string CreatePasswordHash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, KeySize);
        return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    /// <summary>
    /// Verifies a password against a stored hash.
    /// </summary>
    /// <param name="password">The password to verify.</param>
    /// <param name="hashString">The stored password hash to compare against.</param>
    /// <returns>True if the password matches the hash, otherwise false.</returns>
    public static bool VerifyPasswordHash(string password, string hashString)
    {
        var parts = hashString.Split('.', 3);
        if (parts.Length != 3)
            return false;

        var iterations = int.Parse(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var hash = Convert.FromBase64String(parts[2]);

        var inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithm, hash.Length);
        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }
}
