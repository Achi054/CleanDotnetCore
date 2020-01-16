using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Context
{
    public class eComContext : DbContext
    {
        public eComContext(DbContextOptions<eComContext> context)
            : base(context) { }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, Name = "Nike Shoe", Details = "Running Nike shoe", Quantity = 1, TimeStamp = DateTime.UtcNow },
                new Order { Id = 2, Name = "Reebok Shoe", Details = "Running Reebok shoe", Quantity = 2, TimeStamp = DateTime.UtcNow },
                new Order { Id = 3, Name = "Adidas Shoe", Details = "Running Adidas shoe", Quantity = 3, TimeStamp = DateTime.UtcNow });

            base.OnModelCreating(modelBuilder);
        }
    }
}
