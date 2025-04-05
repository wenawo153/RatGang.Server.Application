using RatGang.Server.Users.Database.Entety;
using RatGang.Server.Users.Entety.Request;
using RatGang.Server.Users.Extensions;

namespace RatGang.Server.Users.Services;

public interface IUserService
{
    public Task<User> CreateAsync(CreateUserRequest options);

    public Task<User> GetAsync(Guid id, UserComponents[] components);
    public Task<User> GetAsync(string email, UserComponents[] components);

    public Task<User> EditAsync(EditUserRequest options);
}
