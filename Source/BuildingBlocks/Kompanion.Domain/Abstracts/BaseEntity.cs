using System.Collections.ObjectModel;
using Kompanion.Domain.Exceptions;
using Kompanion.Domain.Interfaces;

namespace Kompanion.Domain.Abstracts;

public abstract class BaseEntity
{
    public abstract Guid Id { get; }

    private List<IDomainEvent> _domainEvents;

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly() ?? ReadOnlyCollection<IDomainEvent>.Empty;

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();

        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public static void CheckRule(IBusinessRule rule, CancellationToken cancellationToken = default)
    {
        if (rule.IsBroken(cancellationToken))
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}