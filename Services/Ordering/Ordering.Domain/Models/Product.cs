
namespace Ordering.Domain.Models
{
    public class Product : Entity<Guid>
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; } = default!;


        public static Product Create (Guid Id, string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            Product product = new Product()
            {
                Id = Id,
                Name = name,
                Price = price
            };
            return product;
        }

    }
}
