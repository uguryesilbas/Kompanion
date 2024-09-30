namespace Kompanion.Domain.Interfaces;

public interface ITrackableEntity
{
    public DateTime CreatedDateTime { get; }
    public DateTime? UpdatedDateTime { get; }
    public Guid CreatedUserId { get; }
    public Guid? UpdatedUserId { get; }
}
