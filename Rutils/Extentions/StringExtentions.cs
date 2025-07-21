namespace Rutils.Extentions;

public static class StringExtentions
{
    public static string FirstCharToUpper(this string s)
    {
        if (string.IsNullOrEmpty(s))
            throw new ArgumentException("There is no first letter");

        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }

    public static string Truncate(this string s, int maxLength, string truncationString = "...")
    {
        maxLength = Math.Clamp(maxLength - truncationString.Length, 0, s.Length);

        return s.Substring(0, maxLength) + truncationString;
    }


    public static int GetWordCount(this string source) => StringHelpers.GetWordCount(source);
    public static TimeSpan GetEstimatedReadTime(this string source, float readingSpeedWordsPerMinute = StringHelpers.AverageReadingSpeedWordsPerMinute) => StringHelpers.GetEstimatedReadTime(source, readingSpeedWordsPerMinute);
}