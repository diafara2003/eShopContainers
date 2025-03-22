
namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicacionDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle
        (CreateOrderCommand command, CancellationToken cancellationToken)
    {

        var order = CreateNewOrder(command.Order);

        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync(cancellationToken);


        var idOrden = order.Id;

        foreach (var item in order.OrderItems)
        {
            dbContext.OrderItems.Add(item );
        }
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(order.Id);
    }

    private Order CreateNewOrder(OrderDTO orderDTO)
    {
        var shippingAddress = Address.Of(orderDTO.ShippingAddress.FirstName, orderDTO.ShippingAddress.LastName, orderDTO.ShippingAddress.EmailAddress, orderDTO.ShippingAddress.Country);
        var billingAddress = Address.Of(orderDTO.BillingAddress.FirstName, orderDTO.BillingAddress.LastName, orderDTO.BillingAddress.EmailAddress, orderDTO.BillingAddress.Country);

        var newOrder = Order.Create(Guid.NewGuid(), orderDTO.CustomerId, orderDTO.OrderName, shippingAddress, billingAddress,
            Payment.Of(orderDTO.Payment.CardNumber, orderDTO.Payment.CardName, orderDTO.Payment.Expiration, orderDTO.Payment.Cvv));

        foreach (var item in orderDTO.OrderItems)
        {
            newOrder.Add(item.ProductId, item.quantity,item.price);
        }

        return newOrder;
    }
}
