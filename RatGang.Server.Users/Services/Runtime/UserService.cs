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
        var user = new User()
        {
            AuthData = new()
            {
                EmailConfirmation = false,
                PasswordHash = options.Password.HashedPassword()
            },
            UserDetails = new()
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                Patronymic = options.Patronymic,
            },
            Email = options.Email,
            Role = options.Role,
            UserName = options.UserName,
        };
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();

        return user;
    }

    public async Task<User> EditAsync(EditUserRequest options)
    {
        var user = await db.Users
            .Include(_ => _.UserDetails)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException("user_is_not_found");

        if (options.UserName != null) user.UserName = options.UserName;
        if (options.Email != null) user.Email = options.Email;

        if (options.FirstName != null) user.UserDetails.FirstName = options.FirstName;
        if (options.LastName != null) user.UserDetails.LastName = options.LastName;
        if (options.Patronymic != null) user.UserDetails.Patronymic = options.Patronymic;

        db.Users.Update(user);
        await db.SaveChangesAsync();

        return user;
    }

    public async Task<User> GetAsync(Guid id)
    {
        return await db.Users
            .AsNoTracking()
            .Include(_ => _.UserDetails)
            .FirstOrDefaultAsync(_ => _.Id == id)
            ?? throw new NullReferenceException("user_not_found");
    }

    public async Task<User> GetAsync(string email)
    {
        return await db.Users
            .AsNoTracking()
            .Include(_ => _.UserDetails)
            .FirstOrDefaultAsync(_ => _.Email == email)
            ?? throw new NullReferenceException("user_not_found");
    }
}
