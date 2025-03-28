﻿
using Builingblock.Messaging.Events;
using MassTransit;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventsHandlers.integration;

class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        //create new order and start order process
        logger.LogInformation("----- Publishing integration event: {IntegrationEventId} ", context.Message.GetType().Name);

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        // Create full order with incoming event data
        var addressDto = new AddressDTO(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine,message.Country,message.State,message.ZipCode);
        var paymentDto = new PaymentDTO(message.CardNumber, message.CardName, message.Expiration, message.CVV,message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDTO(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: Ordering.Domain.Enums.OrderStatus.Pending,
        OrderItems:
        [
                new OrderItemDTO(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                new OrderItemDTO(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)
            ]);

        return new CreateOrderCommand(orderDto);
    }
}
