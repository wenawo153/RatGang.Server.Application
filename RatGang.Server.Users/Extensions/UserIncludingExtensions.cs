using Microsoft.EntityFrameworkCore;
using RatGang.Server.Users.Database.Entety;

namespace RatGang.Server.Users.Extensions;

public static class UserIncludingExtensions
{
    public static IQueryable<User> IncludingUser(
        this IQueryable<User> query, UserComponents[] components)
    {
        if (components.Contains(UserComponents.UserInform))
            query = query.Include(_ => _.UserInformation);

        if (components.Contains(UserComponents.UserDetails))
            query = query.Include(_ => _.UserDetails);

        if (components.Contains(UserComponents.UserAuthMethods))
            query = query.Include(_ => _.AuthMethods)
                .ThenInclude(_ => _.EmailAuthMethod);

        return query;
    }
}

public enum UserComponents
{
    UserInform,
    UserDetails,
    UserAuthMethods
}