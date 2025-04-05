using RatGang.Server.Authentication.Entety;

namespace RatGang.Server.Users.Entety.Responce;

public class UserLoginResponce
{
    public UserResponce UserResponce { get; set; } = new();
    public TokensResponce TokensResponce { get; set; } = new();
}
