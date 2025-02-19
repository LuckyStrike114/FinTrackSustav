using Microsoft.EntityFrameworkCore;

namespace FinTrackSustav.Models
{
    public class FinTrackContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FinancialGoal> FinancialGoals { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.\\fintrack.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}