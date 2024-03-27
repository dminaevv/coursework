namespace Minaev.Tools.Extensions;

public static class EnumerableExtensions
{
    public static Boolean OneOf<T>(this T source, params T[] list)
    {
        return list.Contains(source);
    }

    public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] elements)
    {
        return elements.SelectMany(e => e);
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> sequence)
    {
        return sequence.Where(e => e is not null)!;
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> sequence)
        where T : struct
    {
        return sequence.Where(e => e is not null).Select(e => e!.Value);
    }

}