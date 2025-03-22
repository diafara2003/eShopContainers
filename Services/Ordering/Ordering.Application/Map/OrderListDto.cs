
namespace Ordering.Application.Map;

public static class OrderListDto
{
    public static List<OrderDTO> MapListOrderToDTO(this List<Order> order)
    {

        List<OrderDTO> result = new();

        foreach (var item in order)
        {
            var orderDto = new OrderDTO(
               Id: item.Id, CustomerId: item.customerId, orderName: item.OrderName,
               ShippingAddress: new AddressDTO(
                   item.ShippingAddress.FirstName,
                   item.ShippingAddress.LastName,
                   item.ShippingAddress.EmailAddress ?? "",
                   item.ShippingAddress.Country),
                 BllingAddress: new AddressDTO(
                      item.BillingAddress.FirstName,
                      item.BillingAddress.LastName,
                      item.BillingAddress.EmailAddress ?? "",
                      item.BillingAddress.Country),
                    Payment: new PaymentDTO(
                        item.Payment.CardNumber,
                        item.Payment.CardHolderName,
                        item.Payment.Expiration,
                        item.Payment.Cvv),
                    Status: item.Status,
                    OrderItems: item.OrderItems
                    .Select(i => new OrderItemDTO(productId: i.ProductId,
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
