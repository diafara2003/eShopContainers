﻿

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteeOrderHandler(IApplicationDbContext dbContext)
            : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var order =await dbContext.Orders.FindAsync([command.OrderId], cancellationToken);

            if (order is null) {
                throw new OrderNotFoundException(command.OrderId);
            }

            dbContext.Orders.Remove(order);

            

            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}
