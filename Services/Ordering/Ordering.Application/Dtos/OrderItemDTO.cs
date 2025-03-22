
namespace Ordering.Application.Dtos;

public record OrderItemDTO(
    Guid ProductId, Guid OrderId, decimal price, int quantity);
