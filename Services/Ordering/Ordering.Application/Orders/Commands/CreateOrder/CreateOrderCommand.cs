

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDTO Order )
    :ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid OrderId);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull();        
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Order.ShippingAddress).NotNull();
        RuleFor(x => x.Order.BillingAddress).NotNull();
        RuleFor(x => x.Order.Payment).NotNull();
        RuleFor(x => x.Order.OrderItems).NotNull().WithMessage("OrderItems should not be empty");
    }
}

