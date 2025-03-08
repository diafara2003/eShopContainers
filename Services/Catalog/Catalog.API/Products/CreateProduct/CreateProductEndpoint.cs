

namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price);

public record CreateProductResponse(Guid Id);



public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();

            var result = await sender.Send(command);

            var response = request.Adapt<CreateProductResponse>();

            return Results.Ok(result.Id);
        })
            .WithName("CreateProduct")
            .Produces<CreateProductRequest>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new product")
            .WithDescription("Create a new product in the catalog"); ;


    }
}
