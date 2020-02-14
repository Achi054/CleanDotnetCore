using System;
using Domain.EFCoreEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityFramework.EntityConfigurations
{
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).IsRequired(true);
            builder.Property(s => s.TimeStamp).IsRequired(true);
            builder.Property(s => s.Name).IsRequired(true);
            builder.Property(s => s.Quantity).IsRequired(true);

            builder.HasData(
                new Order
                {
                    Id = 1,
                    Name = "Nike Shoe",
                    Details = "Running Nike shoe",
                    Quantity = 1,
                    TimeStamp = DateTime.UtcNow
                },
                new Order
                {
                    Id = 2,
                    Name = "Reebok Shoe",
                    Details = "Running Reebok shoe",
                    Quantity = 2,
                    TimeStamp = DateTime.UtcNow
                },
                new Order
                {
                    Id = 3,
                    Name = "Adidas Shoe",
                    Details = "Running Adidas shoe",
                    Quantity = 3,
                    TimeStamp = DateTime.UtcNow
                });
        }
    }
}
