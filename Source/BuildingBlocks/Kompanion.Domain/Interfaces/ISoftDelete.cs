using Kompanion.Domain.Enums;

namespace Kompanion.Domain.Interfaces;

public interface ISoftDelete
{
    EntityStatusType Status { get; }

    void Remove();
}