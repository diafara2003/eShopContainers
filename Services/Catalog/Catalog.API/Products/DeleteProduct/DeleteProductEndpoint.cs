
namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductEndpoint : ICarterModule
    {
        public record DeleteProductResponse(bool IdSuccess);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Product/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            }).WithName("DeleteProduct")
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete a product")
                .WithDescription("Delete a product in the catalog");

        }
    }

}
