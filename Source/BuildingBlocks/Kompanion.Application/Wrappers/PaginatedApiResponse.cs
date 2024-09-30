using Kompanion.Application.Interfaces;

namespace Kompanion.Application.Wrappers;

public sealed record PaginatedApiResponse<T>(IEnumerable<T> Data, int TotalCount, int FilteredCount) : ApiResponse<IEnumerable<T>>(Data), IPaginated<T>
{
    public static PaginatedApiResponse<T> Empty()
    {
        return new PaginatedApiResponse<T>(Enumerable.Empty<T>(), 0, 0);
    }
}