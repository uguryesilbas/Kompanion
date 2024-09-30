using Microsoft.EntityFrameworkCore;

namespace Kompanion.Domain.Interfaces;

public interface IDomainEventsDispatcher
{
    Task DispatchEvents(DbContext context, CancellationToken cancellationToken = default);
}