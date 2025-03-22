using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infraestructure.Data.Configutarions;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).HasConversion(
                        orderId => orderId,
                        dbId => dbId);

        builder.HasOne<Customer>()
          .WithMany()
          .HasForeignKey(o => o.CustomerId)
          .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.Id);

     

        builder.OwnsOne(
           o => o.ShippingAddress, addressBuilder =>
           {
               addressBuilder.Property(a => a.FirstName)
                 .HasMaxLength(50)
                 .IsRequired();

               addressBuilder.Property(a => a.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

               addressBuilder.Property(a => a.EmailAddress)
                   .HasMaxLength(50);

           

               addressBuilder.Property(a => a.Country)
                   .HasMaxLength(50);

             
           });

        builder.OwnsOne(
          o => o.BillingAddress, addressBuilder =>
          {
              addressBuilder.Property(a => a.FirstName)
                 .HasMaxLength(50)
                 .IsRequired();

              addressBuilder.Property(a => a.LastName)
                   .HasMaxLength(50)
                   .IsRequired();

              addressBuilder.Property(a => a.EmailAddress)
                  .HasMaxLength(50);



              addressBuilder.Property(a => a.Country)
                  .HasMaxLength(50);
          });

        builder.OwnsOne(
               o => o.Payment, paymentBuilder =>
               {
                   paymentBuilder.Property(p => p.CardName)
                       .HasMaxLength(50);

                   paymentBuilder.Property(p => p.CardNumber)
                       .HasMaxLength(24)
                       .IsRequired();

                   paymentBuilder.Property(p => p.Expiration)
                       .HasMaxLength(10);

                   paymentBuilder.Property(p => p.CVV)
                       .HasMaxLength(3);

                
               });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(o => o.TotalPrice);
    }
}
