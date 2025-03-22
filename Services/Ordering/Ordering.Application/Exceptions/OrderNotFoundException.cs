
namespace Ordering.Application.Exceptions;

public class OrderNotFoundException : Exception
{
    public OrderNotFoundException(Guid orderId) : base($"Order with id {orderId} was not found")
    {

    }
}
