using System.Security.Cryptography;
using System.Text;

namespace NotificationCenter.Utils;

public static class CryptoUtil
{
    public static string HashPassword(string password)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hash).Replace("-", string.Empty);
    }
}