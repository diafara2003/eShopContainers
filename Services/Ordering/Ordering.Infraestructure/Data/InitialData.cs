
namespace Ordering.Infraestructure.Data;

public static class InitialData
{
    public static List<Customer> Customers =>
        new List<Customer>()
        {
            Customer.Create(Guid.NewGuid(), "John Doe", "john.doe@example.com"),
            Customer.Create(Guid.NewGuid(), "Jane Smith", "jane.smith@example.com"),

        };

    public static List<Product> Products =>
       new List<Product>()
       {
           Product.Create(Guid.NewGuid(), "Product 1", 100),
              Product.Create(Guid.NewGuid(), "Product 2", 200),

       };
}
