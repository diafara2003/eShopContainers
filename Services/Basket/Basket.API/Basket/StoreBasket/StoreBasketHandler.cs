namespace Basket.API.Basket.StoreBasket;

public  record StoreBasketCommand(ShoppingCartRoot Cart):
    ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketValidator:AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not de null");
        RuleFor(x => x.Cart.UserName).NotNull().WithMessage("UserName is required");
    }
}

public class StoreBasketCommandHandler(IBasketRepository respository)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCartRoot cart = command.Cart;

        var response =await respository.StoreBasket(cart,cancellationToken);

        //store basket in database

        //update cache redis

        return new StoreBasketResult(command.Cart.UserName);
    }
}
