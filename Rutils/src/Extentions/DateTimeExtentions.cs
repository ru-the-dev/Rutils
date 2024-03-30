namespace Rutils;

public static class DateTimeExtentions
{
    public static DateTime AddByUnit(this DateTime dateTime, TimeUnit unit, double value)
    {
        switch(unit)
        {
            case TimeUnit.Years:
                ThrowIfIntOverflow(value);
                dateTime = dateTime.AddYears((int)value);
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
                dateTime = dateTime.AddMicroseconds(value / 1_000);
            break;
        }

        return dateTime;
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
