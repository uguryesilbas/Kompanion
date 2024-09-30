namespace Kompanion.Application.Interfaces;

public interface IPaginated<out T>
{
    IEnumerable<T> Data { get; }
    int FilteredCount { get; }
    int TotalCount { get; }
}