
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{


    public record GetOrderbyCustomerResponse (IEnumerable<OrderDTO> Orders);
    public class GetOrderbyCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId,ISender sender) =>
            {

                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));
                var response = result.Adapt<GetOrderbyCustomerResponse>();
                return Results.Ok(response);
            });
        }
    }
}
