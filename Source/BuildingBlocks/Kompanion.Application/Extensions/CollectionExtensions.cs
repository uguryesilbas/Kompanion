namespace Kompanion.Application.Extensions;

public static class CollectionExtensions
{
    public static bool IsNullOrEmpty<T>(this ICollection<T> source)
    {
        return source == null || source.Count == 0;
    }

    public static ICollection<T> RemoveAll<T>(this ICollection<T> source, Func<T, bool> predicate)
    {
        if (source.IsNullOrEmpty())
        {
            return source;
        }

        List<T> items = source.Where(predicate).ToList();

        if (items.IsNullOrEmpty())
        {
            return source;
        }

        items.ForEach(x => source.Remove(x));

        return source;
    }

    public static IList<T> AddRange<T>(this IList<T> source, IEnumerable<T> items)
    {
        foreach (T item in items)
        {
            source.Add(item);
        }

        return source;
    }
}