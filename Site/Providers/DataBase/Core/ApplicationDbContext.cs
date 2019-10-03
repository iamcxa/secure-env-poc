using Microsoft.EntityFrameworkCore;

namespace Demo.Providers.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserInfo> UserInfo { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
    }
}
