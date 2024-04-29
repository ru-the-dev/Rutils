namespace Rutils;

public static class TimeSpanExtentions
{
    public static TimeUnitCount ToDisplayUnit(this TimeSpan timeSpan)
    {
        TimeUnitCount years = timeSpan.ToTimeUnit(TimeUnit.Years);
        if (years.count >= 1f)
        {
            return years;
        }

        TimeUnitCount months = timeSpan.ToTimeUnit(TimeUnit.Months);
        if (months.count >= 1f)
        {
            return months;
        }

        TimeUnitCount days = timeSpan.ToTimeUnit(TimeUnit.Days);
        if (days.count >= 1f)
        {
            return days;
        }

        TimeUnitCount hours = timeSpan.ToTimeUnit(TimeUnit.Hours);
        if (hours.count >= 1f)
        {
            return hours;
        }

        TimeUnitCount minutes = timeSpan.ToTimeUnit(TimeUnit.Minutes);
        if (minutes.count >= 1f)
        {
            return minutes;
        }

        TimeUnitCount seconds = timeSpan.ToTimeUnit(TimeUnit.Seconds);
        if (seconds.count >= 1f)
        {
            return seconds;
        }

        TimeUnitCount milliseconds = timeSpan.ToTimeUnit(TimeUnit.Milliseconds);
        if (milliseconds.count >= 1f)
        {
            return milliseconds;
        }

        TimeUnitCount nanoseconds = timeSpan.ToTimeUnit(TimeUnit.Nanoseconds);
        if (nanoseconds.count >= 1f)
        {
            return nanoseconds;
        }

        TimeUnitCount microseconds = timeSpan.ToTimeUnit(TimeUnit.Microseconds);
        if (microseconds.count >= 1f)
        {
            return microseconds;
        }

        return seconds;
    }

    public static TimeUnitCount ToTimeUnit(this TimeSpan timeSpan, TimeUnit unit)
    {
        TimeUnitCount rv = new TimeUnitCount();
        switch(unit)
        {
            case TimeUnit.Years:
                rv.unit = TimeUnit.Years;
                rv.count = timeSpan.TotalDays / 365;
            break;
            case TimeUnit.Months:
                rv.unit = TimeUnit.Months;
                rv.count = timeSpan.TotalDays / 30.437;
            break;
            case TimeUnit.Weeks:
                rv.unit = TimeUnit.Weeks;
                rv.count = timeSpan.TotalDays / 7;
            break;
            case TimeUnit.Days:
                rv.unit = TimeUnit.Days;
                rv.count = timeSpan.TotalDays;
            break;
            case TimeUnit.Hours:
                rv.unit = TimeUnit.Hours;
                rv.count = timeSpan.TotalHours;
            break;
            case TimeUnit.Minutes:
                rv.unit = TimeUnit.Minutes;
                rv.count = timeSpan.TotalMinutes;
            break;
            default: //intentional fall-through
            case TimeUnit.Seconds:
                rv.unit = TimeUnit.Seconds;
                rv.count = timeSpan.TotalSeconds;
            break;
            case TimeUnit.Milliseconds:
                rv.unit = TimeUnit.Milliseconds;
                rv.count = timeSpan.TotalMilliseconds;
            break;
            case TimeUnit.Microseconds:
                rv.unit = TimeUnit.Microseconds;
                rv.count = timeSpan.TotalMicroseconds;
            break;
            case TimeUnit.Nanoseconds:
                rv.unit = TimeUnit.Nanoseconds;
                rv.count = timeSpan.TotalNanoseconds;
            break;
        }

        return rv;
    }
}
