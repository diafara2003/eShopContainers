
namespace Catalog.API.Products.GetProductById;

public record GetProducByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProducByIdResponse>();
            return Results.Ok(response.Product);
        }).WithName("GetProductById")
        .Produces<Product>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get product by id")
        .WithDescription("Get product by id in the catalog");
    }
}
