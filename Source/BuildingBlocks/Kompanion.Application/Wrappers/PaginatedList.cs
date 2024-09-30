using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Kompanion.Application.Extensions;
using Kompanion.Application.Interfaces;

namespace Kompanion.Application.Wrappers;

public class PaginatedList<T> : IPaginated<T>
{
    public IEnumerable<T> Data { get; }
    public int FilteredCount { get; }
    public int TotalCount { get; }

    public PaginatedList(IEnumerable<T> data, int totalCount, int filteredCount)
    {
        TotalCount = totalCount;
        FilteredCount = filteredCount;
        Data = data;
    }

    public static async Task<PaginatedList<T>> Create(IQueryable<T> source, int totalCount, int skip, int take, string order = "", bool isDescending = false, CancellationToken cancellationToken = default)
    {
        int count = await source.CountAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(order))
        {
            source = source.OrderBy(order, isDescending);
        }

        List<T> items = await source.Skip(skip * take).Take(take).ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, totalCount, count);
    }

    public static async Task<PaginatedList<T>> Create<TKey>(IQueryable<T> source, int totalCount, int skip, int take, Expression<Func<T, TKey>> orderByExpression, bool isDescending = false, CancellationToken cancellationToken = default)
    {
        source = isDescending ? source.OrderByDescending(orderByExpression) : source.OrderBy(orderByExpression);

        return await Create(source, totalCount, skip, take, string.Empty, isDescending, cancellationToken);
    }
}