




namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) 
    : IBasketRepository
{
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellation = default)
    {
        session.Delete<ShoppingCartRoot>(userName);
        await session.SaveChangesAsync(cancellation);
        return true;
    }

    public async Task<ShoppingCartRoot> GetBasket(string userName, CancellationToken cancellation = default)
    {
        var basket = await session.LoadAsync<ShoppingCartRoot>(userName, cancellation);

        if (basket is null)
        {
            throw new BasketNotFountException(userName);
        }
        return basket;
    }

    public async Task<ShoppingCartRoot> StoreBasket(ShoppingCartRoot basket, CancellationToken cancellation = default)
    {
        session.Store(basket);

        await session.SaveChangesAsync(cancellation);

        return basket;
    }
}
