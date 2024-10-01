namespace Kompanion.Domain.Interfaces;

public interface ITrackableEntity
{
    public DateTime CreatedDateTime { get; }
    public DateTime? UpdatedDateTime { get; }
    public int CreatedUserId { get; }
    public int? UpdatedUserId { get; }
}
