using MediatR;

namespace Kompanion.Domain.Interfaces;

public interface IDomainEvent : INotification
{
    DateTime DateOccurred { get; }
}