using Microsoft.EntityFrameworkCore;

namespace OnlineBankingSystem.Entity
{

    public class MyDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<UserAccountActivity> UserAccountActivity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=OnlineBankingSystem;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
