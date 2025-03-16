
using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCartRoot Cart);
public record StoreBasketResponse(string UserName);
public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("Basket", async (StoreBasketRequest request, ISender sender) =>
        {
            var result = await sender.Send(new StoreBasketCommand(request.Cart));
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Ok(response);
        }).WithName("NewBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create basket shopping");
    }
}
