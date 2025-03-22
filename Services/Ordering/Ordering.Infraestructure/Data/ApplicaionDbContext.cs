

using Ordering.Application.Data;
using Ordering.Domain.ValueObjects;
using System.Reflection;

namespace Ordering.Infraestructure.Data;

public class ApplicaionDbContext : DbContext, IApplicacionDbContext
{
    public ApplicaionDbContext(DbContextOptions<ApplicaionDbContext> options) :
        base(options)
    {

    }

    public DbSet<Customer> Customers =>Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
