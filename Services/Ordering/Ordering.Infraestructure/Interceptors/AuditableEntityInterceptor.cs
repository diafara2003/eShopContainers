
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infraestructure.Interceptors;

public  class AuditableEntityInterceptor:SaveChangesInterceptor
{

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        UpdateEntitides(eventData.Context);
        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {

        UpdateEntitides(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntitides(DbContext? context)
    {
        if (context == null) return;
       
    }
}
