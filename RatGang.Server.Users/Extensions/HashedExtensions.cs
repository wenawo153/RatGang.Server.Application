namespace RatGang.Server.Users.Extensions;

public static class HashedExtensions
{
    public static string HashedPassword(this string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
    public static bool VerifyPassword(string logPassword, string dbPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(logPassword, dbPassword);
    }
}
