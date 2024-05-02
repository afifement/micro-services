namespace Basket.API.Basket.StoreBasket;

public record StoreBasketResult(string UserName);
internal class StoreBasketCommandHandler
                 (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto)
               : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    { 
        await DeductDiscountAsync(command.Cart, cancellationToken);

        await repository.StoreBasketAsync(command.Cart, cancellationToken);
     
        return new StoreBasketResult(command.Cart.UserName);
    }

    private  async Task DeductDiscountAsync( ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var discountRequest = new GetDiscountRequest() { ProductName = item.ProductName };
            var coupon = await discountProto.GetDiscountAsync(discountRequest,
                                                              cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}
