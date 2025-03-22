
namespace Ordering.Domain.Models;

public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> _orderItems = new();

    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Guid customerId { get; private set; } = default!;
    public string OrderName { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrdesStatus Status { get; set; } = OrdesStatus.Pending;
    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    private Order()
    {
    }

    public static Order Create (Guid Id, Guid customerId, string orderName, Address shippingAddress, Address billingAddress, Payment payment)
    {
      
        Order order = new Order()
        {
            Id = Id,
            customerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment
        };
        order.AddDomainEvent(new OrderCreatedEvent(order));

       // order._orderItems.AddRange(orderItems);
        return order;
    }

    public  void Update(Guid Id, Guid customerId, string orderName, Address shippingAddress, Address billingAddress, Payment payment, OrdesStatus status)
    {

        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = Status;


        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void Add (Guid productId, decimal price, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        OrderItem orderItem = new OrderItem(productId,  price, quantity);
        _orderItems.Add(orderItem);
    }

    public void Remove(Guid productId)
    {
        OrderItem? orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);

        if (orderItem is null)
        {
            throw new DomainException(productId.ToString());
        }
        _orderItems.Remove(orderItem);
    }

}
