namespace RatGang.Server.Users;

public class Configurate
{
    public static Configurate Singleton { get; set; } = new();
    public string DataBaseConnection { get; set; } = string.Empty;

    public SmtpEmailOptions SmtpEmailOptions { get; set; } = new();
}

public class SmtpEmailOptions
{
    public SmtpOptions SmtpOptions { get; set; } = new();
    public EmailOptions EmailOptions { get; set; } = new();
    public Dictionary<string, string> ImagesUrl { get; set; } = new();
    public NetworkCredential NetworkCredential { get; set; } = new();
}

public class NetworkCredential
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
public class SmtpOptions
{
    public string Host { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;

}
public class EmailOptions
{
    public string Address { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
}