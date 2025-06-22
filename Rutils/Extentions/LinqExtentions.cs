namespace Rutils.Extentions;


public static class LinqExtentions
{
    public static (IEnumerable<T> matches, IEnumerable<T> misses) Split<T>(this IEnumerable<T> source, Predicate<T> predicate)
    {
        List<T> matches = new List<T>();
        List<T> misses = new List<T>();

        foreach (T element in source)
        {
            if (predicate(element))
            {
                matches.Add(element);
            }
            else
            {
                misses.Add(element);
            }
        }

        return (matches, misses);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => ThreadSafeRandom.Random.Next());
    }

    public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize) 
    {
        return source
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / chunkSize)
            .Select(x => x.Select(v => v.Value).ToList());
    }

}