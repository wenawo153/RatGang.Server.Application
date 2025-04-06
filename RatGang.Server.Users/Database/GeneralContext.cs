using Microsoft.EntityFrameworkCore;
using RatGang.Server.Users.Database.Entety;

namespace RatGang.Server.Users.Database
{
    public class GeneralContext : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;

        public DbSet<UserDetails> UserDetails { get; set; } = default!;
        public DbSet<UserAuthData> UserAuthData { get; set; } = default!;

        public GeneralContext(DbContextOptions<GeneralContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
