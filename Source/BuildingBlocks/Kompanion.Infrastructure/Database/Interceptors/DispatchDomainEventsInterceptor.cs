using Microsoft.EntityFrameworkCore.Diagnostics;
using Kompanion.Domain.Interfaces;

namespace Kompanion.Infrastructure.Database.Interceptors;

internal class DispatchDomainEventsInterceptor(IDomainEventsDispatcher domainEventsDispatcher) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        InterceptionResult<int> interceptionResult = base.SavingChanges(eventData, result);

        if (interceptionResult.Result > 0)
        {
            domainEventsDispatcher.DispatchEvents(eventData.Context).GetAwaiter().GetResult();
        }

        return interceptionResult;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        InterceptionResult<int> interceptionResult = await base.SavingChangesAsync(eventData, result, cancellationToken);

        if (interceptionResult.Result > 0)
        {
            await domainEventsDispatcher.DispatchEvents(eventData.Context, cancellationToken);
        }

        return interceptionResult;
    }
}