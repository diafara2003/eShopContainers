
using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.Endpoints
{

    public record GetOrderByNameRequest(string Name);
    public record GetOrderByNameResponse(IEnumerable<OrderDTO> Orders);

    //public class GetOrderByName : ICarterModule
    //{
    //    public void AddRoutes(IEndpointRouteBuilder app)
    //    {
    //        app.MapGet("/orders/{orderName}", async (string orderName,ISender sender) => {

    //            var result =await sender.Send(new GetOrderByNameQuery(orderName));

    //            var response = result.Adapt<GetOrderByNameResponse>();

    //            return Results.Ok(response);
    //        });
    //    }
    //}
}
