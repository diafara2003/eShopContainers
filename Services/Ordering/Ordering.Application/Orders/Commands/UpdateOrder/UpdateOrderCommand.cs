
using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{

    public record UpdateOrderCommand(OrderDTO Order)
        :ICommand<UpdateOrderResult>;


    public record UpdateOrderResult(bool IsSuccess);
    

    public  class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order).NotNull();
            RuleFor(x => x.Order.Id).NotEmpty();
            
        }
    }
    
}
