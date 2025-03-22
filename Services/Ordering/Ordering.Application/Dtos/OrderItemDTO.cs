
namespace Ordering.Application.Dtos;

public record OrderItemDTO(
    Guid productId,Guid OrderId, decimal price, int quantity);
