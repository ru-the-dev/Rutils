namespace Rutils;

public static class DateTimeExtentions
{
    public static DateTime EstNow { get => DateTime.UtcNow.UtcToEst(); }
    public static DateTime EstToday { get => DateTime.UtcNow.UtcToEst().Date; }

    public static DateTime AddByUnit(this DateTime dateTime, TimeUnit unit, double value)
    {
        switch(unit)
        {
            case TimeUnit.Years:
                ThrowIfIntOverflow(value);
                dateTime = dateTime.AddYears((int)value);
            break;
            case TimeUnit.Quarters:
                dateTime = dateTime.AddMonths((int)(value * 3));
            break;
            case TimeUnit.Months:
                ThrowIfIntOverflow(value);
                dateTime = dateTime.AddMonths((int)value);
            break;
            case TimeUnit.Weeks:
                dateTime = dateTime.AddDays(value * 7);
            break;
            case TimeUnit.Days:
                dateTime = dateTime.AddDays(value);
            break;
            case TimeUnit.Hours:
                dateTime = dateTime.AddHours(value);
            break;
            case TimeUnit.Minutes:
                dateTime = dateTime.AddMinutes(value);
            break;
            default: //intentional fall-through
            case TimeUnit.Seconds:
                dateTime = dateTime.AddSeconds(value);
            break;
            case TimeUnit.Milliseconds:
                dateTime = dateTime.AddMilliseconds(value);
            break;
            case TimeUnit.Microseconds:
                dateTime = dateTime.AddMicroseconds(value);
            break;
            case TimeUnit.Nanoseconds:
                dateTime = dateTime.AddMicroseconds(value / 1_000d);
            break;
        }

        return dateTime;
    }

    public static int GetQuarter(this DateTime dateTime)
    {
        return (int)((dateTime.Month + 2d) / 3d);
    }

    public static DateTime EstToLocal(this DateTime estDateTime)
    {
        TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        return TimeToLocal(estDateTime, easternZone);
    }

    
    public static DateTime TimeToLocal(this DateTime dateTime, TimeZoneInfo sourceTimeZone)
    {
        return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, TimeZoneInfo.Local);
    }

    public static DateTime UtcToEst(this DateTime utc)
    {
        TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        
        return TimeZoneInfo.ConvertTimeFromUtc(utc, easternZone);
    }

    public static DateTime EstToUtc(this DateTime dateTime)
    {
        TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        return TimeZoneInfo.ConvertTimeToUtc(dateTime, easternZone);
    }

    public static long ToUnixTimeSeconds(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
    }

    public static long ToUnixTimeMilliseconds(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
    }

    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static void ThrowIfIntOverflow(double value)
    {
        if (value > int.MaxValue)
        {
            throw new ArgumentOutOfRangeException("Value is bigger than max int value. Would cause overflow.");
        }
    }
}
