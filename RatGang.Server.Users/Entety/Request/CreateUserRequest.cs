using System.ComponentModel.DataAnnotations;

namespace RatGang.Server.Users.Entety.Request;

public class CreateUserRequest
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;

    public DateTime Birthday { get; set; }

    public string Role { get; set; } = string.Empty;
}