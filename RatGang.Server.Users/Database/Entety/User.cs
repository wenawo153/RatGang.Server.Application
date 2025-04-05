namespace RatGang.Server.Users.Database.Entety;

public class User
{
    public Guid Id { get; set; }

    public UserInformation UserInformation { get; set; } = default!;
    public UserDetails UserDetails { get; set; } = default!;

    public UserAuthMethods AuthMethods { get; set; } = default!;
}
