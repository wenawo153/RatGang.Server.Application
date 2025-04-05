namespace RatGang.Server.Authentication.Entety;

public class TokensRequest
{
    public Guid UserId { get; set; }
    public string Role { get; set; } = string.Empty;
}
