namespace CP2.Architecture;

using System.Security.Cryptography;
using System.Text;


public sealed class SecureHashService
{
    private readonly byte[] _secretKey;

    public SecureHashService(string secretKey)
    {
        _secretKey = Encoding.UTF8.GetBytes(secretKey);
    }

    public string Hash(string text)
    {
        using var hmac = new HMACSHA256(_secretKey);
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(hmac.ComputeHash(bytes));
    }

    public bool Validate(string text, string storedHash)
    {
        string computedHash = Hash(text);

        return CryptographicOperations.FixedTimeEquals(
            Convert.FromBase64String(computedHash),
            Convert.FromBase64String(storedHash)
        );
    }
}


