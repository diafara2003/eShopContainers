
using Builingblock.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.ChekoutBasket;

public record CheckoutBasketCommand(BasketCheckoutBasketDTO BasketCheckoutBasketDTO)
    :ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandvalidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandvalidator()
    {
        RuleFor(x => x.BasketCheckoutBasketDTO).NotNull();
        RuleFor(x => x.BasketCheckoutBasketDTO.UserName).NotNull();
    }
}


public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(command.BasketCheckoutBasketDTO.UserName, cancellationToken);

        if (basket == null)
        {
            return new CheckoutBasketResult(false);
        }

        var eventMessage = command.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasket(command.BasketCheckoutBasketDTO.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}