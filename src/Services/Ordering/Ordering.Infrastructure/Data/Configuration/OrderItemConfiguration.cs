namespace Ordering.Infrastructure.Data.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(orderItemId => orderItemId.Value,
                                                  dbId => OrderItemId.Of(dbId) );

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(oi => oi.ProductId); 

        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.Price).IsRequired();
    }
}
