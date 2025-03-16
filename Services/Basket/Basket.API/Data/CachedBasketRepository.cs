
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data;

public class CachedBasketRepository
    (IBasketRepository repository, IDistributedCache cached)
    : IBasketRepository
{
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellation = default)
    {
        await repository.DeleteBasket(userName, cancellation);
        await cached.RemoveAsync(userName, cancellation);
        return true;
    }

    public async Task<ShoppingCartRoot> GetBasket(string userName, CancellationToken cancellation = default)
    {
        var cachedBasket = await cached.GetStringAsync(userName, cancellation);

        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCartRoot>(cachedBasket)!;

        var basket = await repository.GetBasket(userName, cancellation);
        await cached.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellation);
        return basket;
    }

    public async Task<ShoppingCartRoot> StoreBasket(ShoppingCartRoot basket, CancellationToken cancellation = default)
    {
        await repository.StoreBasket(basket, cancellation);
        await cached.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellation);
        return basket;
    }
}
