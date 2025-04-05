namespace RatGang.Server.Users.Database.Entety;

public class UserDetails
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;

    public long Birthday { get; set; } = 0;

    public User User { get; set; } = default!;
}