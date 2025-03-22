

using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Data;

public interface IApplicacionDbContext
{
    DbSet<Order> Orders { get;  }
    DbSet<OrderItem> OrderItems { get;  }
    DbSet<Payment> Payments { get; }    
    DbSet<Customer> Customers { get;  }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
