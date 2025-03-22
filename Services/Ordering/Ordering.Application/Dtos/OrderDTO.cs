
using Ordering.Domain.Enums;

namespace Ordering.Application.Dtos;

public record OrderDTO(
    Guid Id, 
    Guid customerId,
    string orderName, 
    AddressDTO ShippingAddress, 
    AddressDTO BllingAddress,
    PaymentDTO Payment,
    OrdesStatus Status,
    List<OrderItemDTO> OrderItems);
