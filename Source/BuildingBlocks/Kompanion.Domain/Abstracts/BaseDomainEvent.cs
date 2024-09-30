using Kompanion.Domain.Extensions;
using Kompanion.Domain.Interfaces;

namespace Kompanion.Domain.Abstracts;

public abstract class BaseDomainEvent : IDomainEvent
{
    protected BaseDomainEvent()
    {
        DateOccurred = DateTimeExtensions.Now;
    }

    public DateTime DateOccurred { get; }
}