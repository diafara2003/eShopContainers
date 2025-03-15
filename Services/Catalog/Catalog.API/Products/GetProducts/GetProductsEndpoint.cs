
namespace Catalog.API.Products.GetProducts;
public record GetPtoductRequest(int? pageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> Products);
public class GetProductsEndpoint : ICarterModule
{   

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetPtoductRequest request,ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();


            var result = await sender.Send(query);

            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response.Products );
        }).WithName("GetProducts")
        .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get products ")
        .WithDescription("Get products in the catalog");
    }
}
