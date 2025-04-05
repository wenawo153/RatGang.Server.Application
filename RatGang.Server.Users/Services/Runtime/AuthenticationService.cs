using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RatGang.Server.Users.Database;
using RatGang.Server.Users.Database.Entety;
using RatGang.Server.Users.Database.Entety.AuthMethods;
using RatGang.Server.Users.Entety.Request.AuthRequests;
using RatGang.Server.Users.Extensions;

namespace RatGang.Server.Users.Services.Runtime;

public class AuthenticationService(
    GeneralContext db) : IAuthenticationService
{
    public async Task AddEmailAuthMethodAsync(User user, EmailAuthRequest options)
    {
        if (db.EmailAuthMethods.Any(_ => _.Email == options.Email))
            throw new ArgumentException("email_is_avalaible");
        try
        {
            var method = new EmailAuthMethod()
            {
                Email = options.Email,
                PasswordHash = options.Password.HashedPassword()
            };

            user.AuthMethods.EmailAuthMethod = method;
            db.Users.Update(user);
            await db.SaveChangesAsync();
        }
        catch { throw new Exception("add_email_method_exception"); }
    }

    public async Task<bool> AuthWithEmailAsync(EmailAuthRequest options)
    {
        var model = await db.EmailAuthMethods
            .FirstOrDefaultAsync(_ => _.Email == options.Email)
            ?? throw new ArgumentException("invalid_email");

        if (HashedExtensions.VerifyPassword(options.Password, model.PasswordHash))
            return true;
        return false;
    }
}
