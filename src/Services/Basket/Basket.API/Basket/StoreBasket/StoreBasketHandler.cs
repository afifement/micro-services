namespace Basket.API.Basket.StoreBasket;


public record StoreBasketResult(string UserName);
internal class StoreBasketCommandHandler(IDocumentSession session)
               : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart cart = request.Cart;
        //save database
        session.Store(cart);
        await session.SaveChangesAsync(cancellationToken);
        //return result 
        return new StoreBasketResult("swn");
    }
}
