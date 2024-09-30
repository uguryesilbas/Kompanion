namespace Kompanion.Application.Interfaces;

public interface IOrderable
{
    public string OrderBy { get; init; }
    public bool IsDescending { get; init; }
}