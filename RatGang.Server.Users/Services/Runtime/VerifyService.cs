using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RatGang.Server.Users.Database;
using RatGang.Server.Users.Database.Entety;
using RatGang.Server.Users.Extensions;

namespace RatGang.Server.Users.Services.Runtime;

public class VerifyService(
    GeneralContext db,
    IMemoryCache memoryCache) : IVerifyService
{
    public async Task<string> SendVerityEmail(string email)
    {
        var code = CreateCode(email);
        await SmtpExtensions.SendAsync(new()
        {
            Body = code,
            RecipientEmail = email,
            Subject = "Confirmaion email"
        });

        return code;
    }

    public async Task<User> VerificationEmail(string email, string code)
    {
        if (!memoryCache.TryGetValue(email, out string? verifyCode))
            throw new NullReferenceException("not_avalaible_verify_code");
        if (verifyCode == code) throw new ArgumentException("invalid_code");

        var user = await db.Users
            .Include(_ => _.UserDetails)
            .FirstOrDefaultAsync(_ => _.Email == email) 
            ?? throw new NullReferenceException("user_not_found");

        if (user.AuthData.EmailConfirmation == false)
        {
            user.AuthData.EmailConfirmation = true;
            db.Users.Update(user);
            await db.SaveChangesAsync();
        }

        return user;
    }

    private string CreateCode(string email)
    {
        string code = RandomNumberGenerator.GetInt32(99999).ToString("D5");

        memoryCache.Set(email, code, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });

        return code;
    }
}
