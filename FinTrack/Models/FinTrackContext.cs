using Microsoft.EntityFrameworkCore;
using System.IO;


namespace FinTrackSustav.Models
{
    public class FinTrackContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FinancialGoal> FinancialGoals { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TotalAmount> totalAmounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={Path.Combine(AppContext.BaseDirectory, "fintrack.db")}");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}