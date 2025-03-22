
namespace Ordering.Domain.Models;

public class OrderItem:Entity<Guid>
{
    public Guid Id { get; private set; } = default!;
    public Guid ProductId { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    internal OrderItem(Guid productId, decimal price, int quantity)
    {
        ProductId = productId;
      
        Price = price;
        Quantity = quantity;
    }
}
