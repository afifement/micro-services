namespace Ordering.Infrastructure.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(orderId => orderId.Value,
                                                  dbId => OrderId.Of(dbId));
        builder.HasOne<Customer>()
              .WithMany()
              .HasForeignKey(p => p.CustomerId)
              .IsRequired();

        builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(p => p.OrderName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                       .HasColumnName(nameof(Order.OrderName))
                       .HasMaxLength(100)
                       .IsRequired();
        });

        builder.ComplexProperty(p => p.ShippingAddress, adressBuilder =>
        {
            adressBuilder.Property(n => n.FirstName) 
                       .HasMaxLength(50)
                       .IsRequired();

            adressBuilder.Property(n => n.LastName)
                       .HasMaxLength(50)
                       .IsRequired();

            adressBuilder.Property(n => n.EmailAddress)
                       .HasMaxLength(50);

            adressBuilder.Property(n => n.AddressLine)
                      .HasMaxLength(180)
                      .IsRequired();

            adressBuilder.Property(n => n.Country)
                       .HasMaxLength(50);

            adressBuilder.Property(n => n.State)
                       .HasMaxLength(50);

            adressBuilder.Property(n => n.ZipCode)
                     .HasMaxLength(5)
                     .IsRequired();

        });


        builder.ComplexProperty(p => p.BillingAddress, adressBuilder =>
        {
            adressBuilder.Property(n => n.FirstName)
                       .HasMaxLength(50)
                       .IsRequired();

            adressBuilder.Property(n => n.LastName)
                       .HasMaxLength(50)
                       .IsRequired();

            adressBuilder.Property(n => n.EmailAddress)
                       .HasMaxLength(50);

            adressBuilder.Property(n => n.AddressLine)
                      .HasMaxLength(180)
                      .IsRequired();

            adressBuilder.Property(n => n.Country)
                       .HasMaxLength(50);

            adressBuilder.Property(n => n.State)
                       .HasMaxLength(50);

            adressBuilder.Property(n => n.ZipCode)
                     .HasMaxLength(5)
                     .IsRequired();

        });


        builder.ComplexProperty(p => p.Payment, paymentBuilder =>
        { 

            paymentBuilder.Property(n => n.CardName)
                       .HasMaxLength(50);

            paymentBuilder.Property(n => n.CardNumber)
                      .HasMaxLength(24)
                      .IsRequired();

            paymentBuilder.Property(n => n.Expiration)
                       .HasMaxLength(10);

            paymentBuilder.Property(n => n.CardNumber)
                       .HasMaxLength(3);

            paymentBuilder.Property(n => n.PaymentMethod);

        });

        builder.Property(o=>o.Status)
               .HasDefaultValue(OrderStatus.Draft)
               .HasConversion(
               s=> s.ToString(),
               dbstatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbstatus));

        builder.Property(n => n.TotalPrice);
    }
}
