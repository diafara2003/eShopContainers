
namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardNumber { get; set; } = default!;
        public string CardName { get; set; } = default!;
        public string Expiration { get; set; } = default!;
        public string Cvv { get; set; } = default!;



        protected Payment()
        {

        }

        private Payment(string cardNumber, string cardHolderName, string expiration, string cvv)
        {
            CardNumber = cardNumber;
            CardName = cardHolderName;
            Expiration = expiration;
            Cvv = cvv;
        }

        public static Payment Of(string cardNumber, string cardHolderName, string expiration, string cvv)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);            
            ArgumentException.ThrowIfNullOrWhiteSpace(expiration);
            
            return new Payment(cardNumber, cardHolderName, expiration, cvv);
        }
    }
}