using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Kompanion.Domain.Abstracts;
using Kompanion.Domain.Interfaces;

namespace Kompanion.Domain.Dispatchers;

public class DomainEventsDispatcher(IPublisher publisher) : IDomainEventsDispatcher
{
    public async Task DispatchEvents(DbContext context, CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<IDomainEvent> domainEvents = GetAllDomainEvents(context);

        ClearAllDomainEvents(context);

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }
    }

    private static void ClearAllDomainEvents(DbContext context)
    {
        IEnumerable<EntityEntry<BaseEntity>> entries = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.DomainEvents is { Count: > 0 });

        foreach (EntityEntry<BaseEntity> entry in entries)
        {
            entry.Entity.ClearDomainEvents();
        }
    }

    private static IReadOnlyCollection<IDomainEvent> GetAllDomainEvents(DbContext context)
    {
        return context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.DomainEvents is { Count: > 0 })
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList().AsReadOnly();
    }
}