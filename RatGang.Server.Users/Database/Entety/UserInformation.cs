using Microsoft.EntityFrameworkCore;

namespace RatGang.Server.Users.Database.Entety;

[Index(nameof(Email), IsUnique = true)]
public class UserInformation
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public User User { get; set; } = default!;
}
