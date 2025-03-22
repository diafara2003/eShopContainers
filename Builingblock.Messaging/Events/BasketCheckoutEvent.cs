
namespace Builingblock.Messaging.Events
{
    public record BasketCheckoutEvent : IntegrationEvent
    {
        public string UserName { get; set; } = default!;
        public Guid CustomerId { get; set; } = default!;
        public decimal TotalPrice { get; set; } = default!;

        //shipping and billingaddress
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string State { get; set; } = default!;
        //payment
        public string CardName { get; set; } = default!;
        public string CardNumbe { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public int PaymentMethod { get; set; } = default!;
    }
}
