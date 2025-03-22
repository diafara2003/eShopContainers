
namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicacionDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle
        (CreateOrderCommand command, CancellationToken cancellationToken)
    {

        var order = CreateNewOrder(command.Order);

        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id);
    }

    private Order CreateNewOrder(OrderDTO orderDTO)
    {
        var shippingAddress = Address.Of(orderDTO.ShippingAddress.FirstName, orderDTO.ShippingAddress.LastName, orderDTO.ShippingAddress.EmailAddress, orderDTO.ShippingAddress.Country);
        var billingAddress = Address.Of(orderDTO.BllingAddress.FirstName, orderDTO.BllingAddress.LastName, orderDTO.BllingAddress.EmailAddress, orderDTO.BllingAddress.Country);

        var newOrder = Order.Create(Guid.NewGuid(), orderDTO.CustomerId, orderDTO.orderName, shippingAddress, billingAddress,
            Payment.Of(orderDTO.Payment.CardNumber, orderDTO.Payment.CardHolderName, orderDTO.Payment.Expiration, orderDTO.Payment.Cvv));

        foreach (var item in orderDTO.OrderItems)
        {
            newOrder.Add(item.productId, item.price, item.quantity);
        }

        return newOrder;
    }
}
