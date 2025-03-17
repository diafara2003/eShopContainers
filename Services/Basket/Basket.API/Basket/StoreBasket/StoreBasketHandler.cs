using static Discount.Grpc.DiscountProtoService;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCartRoot Cart) :
    ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not de null");
        RuleFor(x => x.Cart.UserName).NotNull().WithMessage("UserName is required");
    }
}

public class StoreBasketCommandHandler(IBasketRepository respository, DiscountProtoServiceClient discount)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        //communicate with discount Grpc and calculate lastest price of products in basket

        await DeductDiscount(command.Cart, cancellationToken);

        //Store basket in database and update cache redis
        var response = await respository.StoreBasket(command.Cart, cancellationToken);

        //store basket in database

        //update cache redis

        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCartRoot Cart, CancellationToken cancelationToken)
    {
        foreach (var item in Cart.Items)
        {
            var coupon = await discount.GetDiscountAsync(new Discount.Grpc.GetDiscountRequest
            {
                ProductName = item.ProductName
            }, cancellationToken: cancelationToken);

            item.Price -= coupon.Amount;
        }
    }
}
