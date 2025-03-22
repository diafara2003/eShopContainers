
namespace Ordering.Domain.ValueObjects
{
    public  record Address
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? EmailAddress { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

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
