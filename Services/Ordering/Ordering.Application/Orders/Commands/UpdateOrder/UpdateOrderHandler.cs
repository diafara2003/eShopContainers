
namespace Ordering.Application.Orders.Commands.UpdateOrder;

class UpdateOrderHandler(IApplicacionDbContext dbContext) :
    ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, 
        CancellationToken cancellationToken)
    {
        var orderId = command.Order.Id;
        var order = await dbContext.Orders.FindAsync([orderId],cancellationToken);
        
        if(order is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        UpdateOrderWithNewValues(order, command.Order);

        dbContext.Orders.Update(order);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    private void UpdateOrderWithNewValues(Order order,OrderDTO orderDto)
    {
        var updateShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.Country);
        var updateBillingAddress = Address.Of(orderDto.BllingAddress.FirstName, orderDto.BllingAddress.LastName, orderDto.BllingAddress.EmailAddress, orderDto.BllingAddress.Country);
        var updatePayment = Payment.Of(orderDto.Payment.CardNumber, orderDto.Payment.CardHolderName, orderDto.Payment.Expiration, orderDto.Payment.Cvv);

        order.Update(order.Id, orderDto.CustomerId, orderDto.orderName, updateShippingAddress, updateBillingAddress, updatePayment, orderDto.Status);
    }
}
