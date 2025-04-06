using Microsoft.EntityFrameworkCore;

namespace RatGang.Server.Users.Database.Entety;

public class UserAuthData
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string PasswordHash { get; set; } = string.Empty;
    public bool EmailConfirmation { get; set; } = false;

    public User User { get; set; } = default!;
}
