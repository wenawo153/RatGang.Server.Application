using Microsoft.EntityFrameworkCore;
using RatGang.Server.Users.Database.Entety;
using RatGang.Server.Users.Database.Entety.AuthMethods;

namespace RatGang.Server.Users.Database
{
    public class GeneralContext : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;

        public DbSet<UserInformation> UserInformation { get; set; } = default!;
        public DbSet<UserDetails> UserDetails { get; set; } = default!;

        public DbSet<UserAuthMethods> AuthMethods { get; set; } = default!;
        public DbSet<EmailAuthMethod> EmailAuthMethods { get; set; } = default!;

        public GeneralContext(DbContextOptions<GeneralContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
