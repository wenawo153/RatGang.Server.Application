using Microsoft.EntityFrameworkCore;

namespace RatGang.Server.Users.Database.Entety;

[Index(nameof(UserName))]
[Index(nameof(Role))]
[Index(nameof(Email), IsUnique = true)]
public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public UserDetails UserDetails { get; set; } = default!;

    public UserAuthData AuthData { get; set; } = default!;
}
