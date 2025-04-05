using Microsoft.EntityFrameworkCore;

namespace RatGang.Server.Users.Database.Entety.AuthMethods;

[Index(nameof(Email))]
public abstract class AuthMethod
{
    public Guid Id { get; set; }
    public Guid UserAuthMethodsId { get; set; }

    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmation { get; set; } = false;

    public UserAuthMethods UserAuthMethods { get; set; } = default!;
}
