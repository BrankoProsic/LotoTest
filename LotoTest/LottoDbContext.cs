using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LotoTest
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
