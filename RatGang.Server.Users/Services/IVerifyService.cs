using RatGang.Server.Users.Database.Entety;

namespace RatGang.Server.Users.Services;

public interface IVerifyService
{
    public Task<string> SendVerityEmail(string email);
    public Task<User> VerificationEmail(string email, string code);
}
