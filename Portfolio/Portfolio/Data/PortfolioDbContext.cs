using Microsoft.EntityFrameworkCore;
using Portfolio.Entities;

namespace Portfolio.Data
{
    public class PortfolioDbContext : DbContext
    {
        public DbSet<Cash> Cashes => Set<Cash>();

        public DbSet<Bond> Bonds => Set<Bond>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb"); 
        }

    }
}
