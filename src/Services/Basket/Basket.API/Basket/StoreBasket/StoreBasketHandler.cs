namespace Basket.API.Basket.StoreBasket;


public record StoreBasketResult(string UserName);
internal class StoreBasketCommandHandler(IBasketRepository repository)
               : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart cart = request.Cart;
        await repository.StoreBasketAsync(cart, cancellationToken); 
        //return result 
        return new StoreBasketResult(cart.UserName);
    }
}
