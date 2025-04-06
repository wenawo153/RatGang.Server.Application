using Microsoft.EntityFrameworkCore;
using RatGang.Server.Users.Database;
using RatGang.Server.Users.Entety.Request.AuthRequests;
using RatGang.Server.Users.Extensions;

namespace RatGang.Server.Users.Services.Runtime;

public class AuthenticationService(
    GeneralContext db) : IAuthenticationService
{
    public async Task<bool> AuthWithEmailAsync(EmailAuthRequest options)
    {
        var user = await db.Users
            .Include(_ => _.AuthData)
            .FirstOrDefaultAsync(_ => _.Email == options.Email)
            ?? throw new ArgumentException("invalid_email");

        if (HashedExtensions.VerifyPassword(options.Password, user.AuthData.PasswordHash))
            return true;
        return false;
    }
}
