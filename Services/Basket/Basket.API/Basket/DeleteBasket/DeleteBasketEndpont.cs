
using Basket.API.Basket.StoreBasket;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketRequerst(string UserName);

public record DeleteBAsketResponse(bool IsSuccess);

public class DeleteBasketEndpont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string UserName, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(UserName));

            var response = result.Adapt<DeleteBAsketResponse>();

            return Results.Ok(response);
        }).WithName("DeleteProduct")
        .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product")
        .WithDescription("Delete basket shopping");
    }
}
