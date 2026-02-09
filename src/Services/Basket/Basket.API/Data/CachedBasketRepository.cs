global using Microsoft.Extensions.Caching.Distributed;
global using System.Text.Json;

namespace Basket.API.Data;

public class CachedBasketRepository 
    (IBasketRepository repository, IDistributedCache cache)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

        if (!string.IsNullOrEmpty(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }

        var basket =  await repository.GetBasketAsync(userName, cancellationToken);

        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        
        await repository.StoreBasket(basket, cancellationToken);

        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        await repository.DeleteBasketAsync(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }

    


}
