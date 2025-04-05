using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using RatGang.Server.Users.Database;
using RatGang.Server.Users.Database.Entety;
using RatGang.Server.Users.Entety.Request;
using RatGang.Server.Users.Extensions;

namespace RatGang.Server.Users.Services.Runtime;

public class UserService(GeneralContext db) : IUserService
{
    public async Task<User> CreateAsync(CreateUserRequest options)
    {
        if (!await IsLegacyEmail(options.Email))
            throw new Exception("email_is_avalaible");

        var user = new User()
        {
            AuthMethods = new()
            {
                EmailAuthMethod = new()
            },
            UserDetails = new()
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                Patronymic = options.Patronymic,
                Birthday = options.Birthday,
            },
            UserInformation = new()
            {
                Role = options.Role,
                Email = options.Email,
                UserName = options.UserName,
            }
        };
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();

        return user;
    }

    public async Task<User> EditAsync(EditUserRequest options)
    {
        var user = await db.Users
            .Include(_ => _.UserInformation)
            .Include(_ => _.UserDetails)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException("user_is_not_found");

        if (options.Email != null) user.UserInformation.Email = options.Email;
        if (options.UserName != null) user.UserInformation.UserName = options.UserName;

        if (options.FirstName != null) user.UserDetails.FirstName = options.FirstName;
        if (options.LastName != null) user.UserDetails.LastName = options.LastName;
        if (options.Patronymic != null) user.UserDetails.Patronymic = options.Patronymic;
        if (options.Birthday != null) user.UserDetails.Birthday = (long)options.Birthday;

        db.Users.Update(user);
        await db.SaveChangesAsync();

        return user;
    }

    public async Task<User> GetAsync(Guid id, UserComponents[] components)
    {
        return await db.Users
            .AsNoTracking()
            .IncludingUser(components)
            .FirstOrDefaultAsync(_ => _.Id == id)
            ?? throw new NullReferenceException("user_not_found");
    }

    public async Task<User> GetAsync(string email, UserComponents[] components)
    {
        return await db.Users
            .AsNoTracking()
            .IncludingUser(components)
            .Include(_ => _.UserInformation)
            .FirstOrDefaultAsync(_ => _.UserInformation.Email == email)
            ?? throw new NullReferenceException("user_not_found");
    }

    public async Task<User> GetFromEmailAuthAsync(string email, UserComponents[] components)
    {
        return await db.EmailAuthMethods
            .Include(_ => _.UserAuthMethods)
                .ThenInclude(_ => _.User)
            .Where(_ => _.Email == email)
            .Select(_ => _.UserAuthMethods.User)
            .IncludingUser(components)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException("user_not_found");
    }

    private async Task<bool> IsLegacyEmail(string email)
    {
        if (await db.UserInformation.AnyAsync(_ => _.Email == email)
            || await db.EmailAuthMethods.AnyAsync(_ => _.Email == email))
            return false;
        return true;
    }
}
