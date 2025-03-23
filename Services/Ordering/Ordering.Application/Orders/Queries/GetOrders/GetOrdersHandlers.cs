
namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandlers(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersQuery, GetOrdersresult>
    {


        public async Task<GetOrdersresult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .Skip(pageSize * pageIndex)
                .Take(pageIndex)
                .OrderBy(o => o.OrderName)
                .ToListAsync(cancellationToken);

            return new GetOrdersresult(
                new BuildingBlocks.Pagination.PaginationResult<OrderDTO>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    orders.MapListOrderToDTO()
                    ));
        }
    }
}
