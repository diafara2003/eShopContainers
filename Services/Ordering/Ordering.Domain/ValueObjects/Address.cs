
namespace Ordering.Domain.ValueObjects
{
    public  record Address
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? EmailAddress { get; set; } = default!;
        public string Country { get; set; } = default!;

        protected Address()
        {
        }

        private Address(string firstName,string lastName,
            string emailAddress,string country)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Country = country;
        }

        public static Address Of(string firstName, string lastName,
            string emailAddress, string country)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
            ArgumentException.ThrowIfNullOrWhiteSpace(country);

            return new Address(firstName, lastName, emailAddress, country);     
        }

    }
}
