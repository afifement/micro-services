


namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext context) 
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
                           .Include(p => p.OrderItems)
                           .AsNoTracking()
                           .Where(o => o.OrderName.Value.Contains(query.Name))
                           .OrderBy(o => o.OrderName)
                           .ToListAsync(cancellationToken);

        var orderDtos = orders.ToOrderDtoList();
        return new GetOrdersByNameResult(orderDtos);
    }

   
}
