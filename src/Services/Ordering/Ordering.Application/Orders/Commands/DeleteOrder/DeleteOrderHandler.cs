 
namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext)
                             : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderId);
        var order = dbContext.Orders.AsEnumerable().FirstOrDefault(p => p.Id.Value == orderId.Value);
        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteOrderResult(true);

    }
}