
namespace Ordering.Domain.Models;

public class OrderItem:Entity<Guid>
{
    public Guid ProductId { get; private set; } = default!;
    public Guid ProductID { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    internal OrderItem(Guid productId, Guid productID, decimal price, int quantity)
    {
        ProductId = productId;
        ProductID = productID;
        Price = price;
        Quantity = quantity;
    }
}
