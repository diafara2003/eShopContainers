
namespace Ordering.Domain.Models;

public class Customer:Entity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
  

    public static Customer Create(Guid Id,string name,string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        Customer customer = new Customer()
        {
            Id = Id,
            Name = name,
            Email = email
        };
        return customer;
    }
}
