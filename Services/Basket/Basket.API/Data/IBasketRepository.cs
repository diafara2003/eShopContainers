namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCartRoot> GetBasket(string userName,CancellationToken cancellation=default);
    Task<ShoppingCartRoot> StoreBasket(ShoppingCartRoot basket, CancellationToken cancellation = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellation = default);
}
