using RatGang.Server.Authentication.Entety;
using RatGang.Server.Users.Database.Entety;
using RatGang.Server.Users.Entety.Responce;

namespace RatGang.Server.Users.Extensions;

public static class ModelExtensions
{
    public static UserResponce ToResponce(this User user) 
    {
        return new()
        {
            Birthday = new DateTime(user.UserDetails.Birthday),
            Email = user.Email,
            FirstName = user.UserDetails.FirstName,
            LastName = user.UserDetails.LastName,
            Patronymic = user.UserDetails.Patronymic,
            UserName = user.UserName,
        };
    }

    public static UserLoginResponce ToLoginResponce(
        this User user, TokensResponce tokensResponce)
    {
        return new()
        {
            UserResponce = user.ToResponce(),
            TokensResponce = tokensResponce
        };
    }
}
