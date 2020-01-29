using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.EntityConfigurations;

namespace Repository.Context
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
