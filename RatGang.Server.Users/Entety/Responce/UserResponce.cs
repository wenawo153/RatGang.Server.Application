namespace RatGang.Server.Users.Entety.Responce;

public class UserResponce
{
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;

    public DateTime Birthday { get; set; }
}
