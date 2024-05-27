namespace Ordering.Infrastructure.Data.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(p => p.Id);

    }
}
