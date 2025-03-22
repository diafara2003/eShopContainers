
namespace Ordering.Application.Dtos;

public record PaymentDTO(
    string CardNumber,
    string CardName,
    string Expiration,
    string Cvv);

