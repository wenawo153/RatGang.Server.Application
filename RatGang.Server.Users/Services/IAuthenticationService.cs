using RatGang.Server.Users.Database.Entety;
using RatGang.Server.Users.Entety.Request.AuthRequests;

namespace RatGang.Server.Users.Services;

public interface IAuthenticationService
{
    public Task<bool> AuthWithEmailAsync(EmailAuthRequest options);
}
