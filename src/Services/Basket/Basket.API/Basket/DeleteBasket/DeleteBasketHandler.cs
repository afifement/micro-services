
namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResult(bool IsSuccess);
internal class DeleteBasketCommandHandler(IDocumentSession session)
              : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        session.Delete<ShoppingCart>(command.UserName);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteBasketResult(true);
    }
}
