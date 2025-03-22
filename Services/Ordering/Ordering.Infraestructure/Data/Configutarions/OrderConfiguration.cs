using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;

namespace Ordering.Infraestructure.Data.Configutarions;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ComplexProperty(c => c.ShippingAddress);
        builder.ComplexProperty(c => c.BillingAddress);
        builder.ComplexProperty(c => c.Payment);


        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.customerId)
            .IsRequired();

        
    }
}
