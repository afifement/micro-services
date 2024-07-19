namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = context.Orders
                           .Include(p => p.OrderItems)
                           .AsNoTracking()
                           .AsEnumerable()
                           .Where(o => o.OrderName.Value.Contains(query.Name))
                           .OrderBy(o => o.OrderName)
                           .ToList();

        var orderDtos = orders.ToOrderDtoList();
        return Task.FromResult(new GetOrdersByNameResult(orderDtos));
    }


}
