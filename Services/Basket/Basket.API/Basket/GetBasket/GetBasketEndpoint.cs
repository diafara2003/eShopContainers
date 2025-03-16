﻿
namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse (ShoppingCartRoot Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("Basket/{userName}", async (string userName, ISender sender) =>
        {
            var result =await sender.Send(new GetBasketQuery(userName));

            var response = result.Adapt<GetBasketResponse>();   

            Results.Ok(result);
        }).WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get GetBasket ")
        .WithDescription("Get basket shopping"); 
    }
}
