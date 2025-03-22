

using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest)
    : IQuery<GetOrdersresult>;

public record GetOrdersresult(PaginationResult<OrderDTO> Orders);


