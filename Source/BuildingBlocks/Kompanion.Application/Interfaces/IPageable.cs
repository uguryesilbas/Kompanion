namespace Kompanion.Application.Interfaces;

public interface IPageable
{
    public int Skip { get; init; }
    public int Take { get; init; }
}