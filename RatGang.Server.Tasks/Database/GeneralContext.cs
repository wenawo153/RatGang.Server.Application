using Microsoft.EntityFrameworkCore;

namespace RatGang.Server.Tasks.Database
{
    public class GeneralContext : DbContext
    {
        public GeneralContext(DbContextOptions<GeneralContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
