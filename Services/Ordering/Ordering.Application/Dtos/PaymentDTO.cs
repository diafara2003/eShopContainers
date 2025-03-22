
namespace Ordering.Application.Dtos;

public record PaymentDTO(
    string CardNumber,
    string CardHolderName,
    string Expiration,
    string Cvv);

