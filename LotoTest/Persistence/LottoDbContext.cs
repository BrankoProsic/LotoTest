using Microsoft.EntityFrameworkCore;

namespace LotoTest.Persistence
{
    public class LottoDbContext : DbContext
    {
        public DbSet<LotteryCombination> LotteryCombinations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = LottoDB"
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LotteryCombination>().HasKey(e => e.CombinationId);
            modelBuilder.Entity<LotteryCombination>().Ignore(n => n.Numbers);
        }
    }
}
