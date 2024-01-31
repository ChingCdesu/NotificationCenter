using System.Security.Cryptography;
using System.Text;

namespace NotificationCenter.Utils;

public static class RandomUtil
{
    private static ReadOnlySpan<char> SmallLetters => "abcdefghijklmnopqrstuvwxyz";

    private static ReadOnlySpan<char> BigLetters => "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private static ReadOnlySpan<char> Numbers => "0123456789";

    private static ReadOnlySpan<char> SpecialCharacters => "!@#$%^&*()_+-=[]{};':\",./<>?\\|";

    public static string GenerateString(int length = 8, bool useSmallLetters = true, bool useBigLetters = true,
        bool useNumbers = true, bool useSpecialCharacters = false)
    {
        var random = new Random();
        var characters = new List<char>();
        if (useSmallLetters)
        {
            characters.AddRange(SmallLetters);
        }

        if (useBigLetters)
        {
            characters.AddRange(BigLetters);
        }

        if (useNumbers)
        {
            characters.AddRange(Numbers);
        }

        if (useSpecialCharacters)
        {
            characters.AddRange(SpecialCharacters);
        }

        ReadOnlySpan<char> charactersSpan = characters.ToArray();
        return RandomNumberGenerator.GetString(charactersSpan, length);
    }
    
    public static string GenerateSalt(int length = 32)
    {
        return GenerateString(length, true, true, true, true);
    }

    public static string GeneratePassword(int length = 32)
    {
        return GenerateString(length, true, true, true, false);
    }
}