
namespace Ordering.Application.Map;

public static class OrderListDto
{
    public static List<OrderDTO> MapListOrderToDTO(this List<Order> order)
    {

        List<OrderDTO> result = new();

        foreach (var item in order)
        {
            var orderDto = new OrderDTO(
               Id: item.Id, CustomerId: item.customerId, OrderName: item.OrderName,
               ShippingAddress: new AddressDTO(
                   item.ShippingAddress.FirstName,
                   item.ShippingAddress.LastName,
                   item.ShippingAddress.EmailAddress ?? "",
                   item.ShippingAddress.Country),
                 BillingAddress: new AddressDTO(
                      item.BillingAddress.FirstName,
                      item.BillingAddress.LastName,
                      item.BillingAddress.EmailAddress ?? "",
                      item.BillingAddress.Country),
                    Payment: new PaymentDTO(
                        item.Payment.CardNumber,
                        item.Payment.CardName,
                        item.Payment.Expiration,
                        item.Payment.Cvv),
                    Status: item.Status,
                    OrderItems: item.OrderItems
                    .Select(i => new OrderItemDTO(ProductId: i.ProductId,
                    OrderId: i.Id,
                    price: i.Price,
                    quantity: i.Quantity
                       )).ToList()
               );

            result.Add(orderDto);

        }

        return result;

    }
}
