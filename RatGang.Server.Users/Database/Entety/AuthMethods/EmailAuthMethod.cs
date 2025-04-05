namespace RatGang.Server.Users.Database.Entety.AuthMethods;

public class EmailAuthMethod : AuthMethod
{
    public string PasswordHash { get; set; } = string.Empty;
}
