using Microsoft.EntityFrameworkCore;
using RatGang.Server.Tasks.Database.Entety;

namespace RatGang.Server.Tasks.Database
{
    public class GeneralContext : DbContext
    {
        public DbSet<Entety.Task> Tasks { get; set; }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ChangeArticleEvent> Changes { get; set; }

        public GeneralContext(DbContextOptions<GeneralContext> options) : base(options)
        {
            Database.MigrateAsync();
            Database.EnsureCreated();
        }
    }
}
