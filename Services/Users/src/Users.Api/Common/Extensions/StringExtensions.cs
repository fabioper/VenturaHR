namespace Users.Api.Common.Extensions;

public static class StringExtensions
{
    public static string ToHash(this string input)
        => BCrypt.Net.BCrypt.HashPassword(input);

    public static bool IsEqualToHash(this string input, string hash)
        => BCrypt.Net.BCrypt.Verify(input, hash);
}