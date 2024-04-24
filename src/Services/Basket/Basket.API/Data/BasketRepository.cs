namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCart>(userName);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        ShoppingCart? basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
        if (basket == null) throw new BasketNotFoundException(userName);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        session.Store(cart);
        await session.SaveChangesAsync(cancellationToken);
        return cart;
    }
}
