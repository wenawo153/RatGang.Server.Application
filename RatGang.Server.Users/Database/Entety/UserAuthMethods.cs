using RatGang.Server.Users.Database.Entety.AuthMethods;

namespace RatGang.Server.Users.Database.Entety;

public class UserAuthMethods
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public EmailAuthMethod EmailAuthMethod { get; set; } = default!;

    public User User { get; set; } = default!;
}
