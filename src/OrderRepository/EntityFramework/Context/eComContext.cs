using Domain.EFCoreEntities;
using Microsoft.EntityFrameworkCore;
using Repository.EntityFramework.EntityConfigurations;

namespace Repository.EntityFramework.Context
{
    public class eComContext : DbContext
    {
        public eComContext(DbContextOptions<eComContext> context)
            : base(context) { }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
