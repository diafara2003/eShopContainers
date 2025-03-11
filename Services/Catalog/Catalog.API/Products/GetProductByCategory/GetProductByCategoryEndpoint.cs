
namespace Catalog.API.Products.GetProductByCategory;

public class GetProductByCategoryEndpoint : ICarterModule
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", 
            async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByCategoryQuery(category));
            var response = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response.Products);
        })
            .WithName("GetProductByCategory")
            .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get products by category")
            .WithDescription("Get products by category in the catalog");
    }
}
