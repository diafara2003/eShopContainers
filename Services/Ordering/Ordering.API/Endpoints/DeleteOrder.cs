
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{

    public record DeleteOrderRequest(Guid Id);
    public record DeleteOrderResponse(bool IsSuccess);
    //public class DeleteOrder : ICarterModule
    //{
    //    public void AddRoutes(IEndpointRouteBuilder app)
    //    {
    //        app.MapDelete("/order", async (DeleteOrderRequest request,ISender sender) => {


    //            var command = request.Adapt<DeleteOrderCommand>();

    //            var result = await sender.Send(command);

    //            var response = result.Adapt<DeleteOrderResponse>();

    //            return Results.Ok(response);

    //        });
    //    }
    //}
}
