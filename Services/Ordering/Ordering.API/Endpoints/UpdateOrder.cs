﻿

using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints
{

    public record UpdateOrderRequest(OrderDTO Order);
    public record UpdateOrderResponse(bool IsSuccess);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateOrderResponse>();

                return Results.Ok(response);

            });
        }
    }
}
