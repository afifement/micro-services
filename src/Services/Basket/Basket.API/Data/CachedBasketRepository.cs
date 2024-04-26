namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
            : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
        {
            var basket = JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            return basket ?? throw new Exception("Object serialization is failed");
        }
        var basketFromDb= await repository.GetBasketAsync(userName, cancellationToken);
        await cache.SetStringAsync(userName,JsonSerializer.Serialize(basketFromDb),cancellationToken);
        return basketFromDb;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        ShoppingCart basket = await repository.StoreBasketAsync(cart, cancellationToken);
        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), cancellationToken);
        return basket;
    }
    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        bool isSuccess = await repository.DeleteBasketAsync(userName, cancellationToken);
        await cache.RemoveAsync(userName, cancellationToken);
        return isSuccess;
    }

    
}
