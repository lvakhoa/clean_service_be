using System.Security.Cryptography;

namespace CleanService.Src.Utils;

public static class PasswordUtils
{
    private const int SaltSize = 16; // 128-bit
    private const int KeySize = 32; // 256-bit
    private const int Iterations = 10000;

    public static string HashPassword(this string password)
    {
        var salt = new byte[SaltSize];
        RandomNumberGenerator.Fill(salt);

        using var rfc2898 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var hash = rfc2898.GetBytes(KeySize);

        var hashBytes = new byte[SaltSize + KeySize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

        return Convert.ToBase64String(hashBytes);
    }

    public static bool VerifyPassword(this string hashedPassword, string password)
    {
        var hashBytes = Convert.FromBase64String(hashedPassword);

        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        using var rfc2898 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var hash = rfc2898.GetBytes(KeySize);

        for (var i = 0; i < KeySize; i++)
        {
            if (hashBytes[SaltSize + i] != hash[i])
            {
                return false;
            }
        }

        return true;
    }
}
