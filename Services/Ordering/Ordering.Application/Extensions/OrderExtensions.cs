
namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDTO> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => new OrderDTO(
            Id: order.Id,
            CustomerId: order.CustomerId,
            OrderName: order.OrderName,
            ShippingAddress: new AddressDTO(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.BillingAddress.AddressLine,order.ShippingAddress.Country,order.ShippingAddress.State,order.ShippingAddress.ZipCode),
            BillingAddress: new AddressDTO(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
            Payment: new PaymentDTO(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV,order.Payment.PaymentMethod),
            Status: order.Status,
            OrderItems: order.OrderItems.Select(oi => new OrderItemDTO(oi.OrderId, oi.ProductId, oi.Price, oi.Quantity)).ToList()
        ));
    }

    public static OrderDTO ToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    private static OrderDTO DtoFromOrder(Order order)
    {
        return new OrderDTO(
                    Id: order.Id,
                    CustomerId: order.CustomerId,
                    OrderName: order.OrderName,
                    ShippingAddress: new AddressDTO(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
                    BillingAddress: new AddressDTO(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
                    Payment: new PaymentDTO(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                    Status: order.Status,
                    OrderItems: order.OrderItems.Select(oi => new OrderItemDTO(oi.OrderId, oi.ProductId, oi.Price, oi.Quantity)).ToList()
                );
    }
}
