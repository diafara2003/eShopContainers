
namespace Catalog.API.Products.UpdateProduct;

public class UpdateProductEndpoint : ICarterModule
{
    public record UpdateProductRequest(
        Guid Id,
        string Name,
        string Description,
        List<string> Category,
        string ImageFile,
        decimal Price
    );
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<UpdateProductResult>();

            return Results.Ok(response);
        })
        .Produces<UpdateProductResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update a product")
        .WithDescription("Update a product in the catalog");
    }
}
