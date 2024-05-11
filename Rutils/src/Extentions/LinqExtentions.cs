namespace Rutils;


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
}