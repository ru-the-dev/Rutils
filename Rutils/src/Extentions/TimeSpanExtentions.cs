namespace Rutils;

public static class TimeSpanExtentions
{
    public static TimeUnitCount ToDisplayUnit(this TimeSpan timeSpan, TimeUnit[] excludedUnits)
    {
        TimeUnitCount years = timeSpan.ToTimeUnit(TimeUnit.Years);
        if (years.Count >= 1f && excludedUnits.Contains(years.Unit) == false)
        {
            return years;
        }

        TimeUnitCount quarters = timeSpan.ToTimeUnit(TimeUnit.Quarters);
        if (quarters.Count >= 1f && excludedUnits.Contains(quarters.Unit) == false)
        {
            return quarters;
        }

        TimeUnitCount months = timeSpan.ToTimeUnit(TimeUnit.Months);
        if (months.Count >= 1f && excludedUnits.Contains(months.Unit) == false)
        {
            return months;
        }

        TimeUnitCount days = timeSpan.ToTimeUnit(TimeUnit.Days);
        if (days.Count >= 1f && excludedUnits.Contains(days.Unit) == false)
        {
            return days;
        }

        TimeUnitCount hours = timeSpan.ToTimeUnit(TimeUnit.Hours);
        if (hours.Count >= 1f && excludedUnits.Contains(hours.Unit) == false)
        {
            return hours;
        }

        TimeUnitCount minutes = timeSpan.ToTimeUnit(TimeUnit.Minutes);
        if (minutes.Count >= 1f && excludedUnits.Contains(minutes.Unit) == false)
        {
            return minutes;
        }

        TimeUnitCount seconds = timeSpan.ToTimeUnit(TimeUnit.Seconds);
        if (seconds.Count >= 1f && excludedUnits.Contains(seconds.Unit) == false)
        {
            return seconds;
        }

        TimeUnitCount milliseconds = timeSpan.ToTimeUnit(TimeUnit.Milliseconds);
        if (milliseconds.Count >= 1f && excludedUnits.Contains(milliseconds.Unit) == false)
        {
            return milliseconds;
        }

        TimeUnitCount nanoseconds = timeSpan.ToTimeUnit(TimeUnit.Nanoseconds);
        if (nanoseconds.Count >= 1f && excludedUnits.Contains(nanoseconds.Unit) == false)
        {
            return nanoseconds;
        }

        TimeUnitCount microseconds = timeSpan.ToTimeUnit(TimeUnit.Microseconds);
        if (microseconds.Count >= 1f && excludedUnits.Contains(microseconds.Unit) == false)
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
                rv.Unit = TimeUnit.Years;
                rv.Count = timeSpan.TotalDays / 365;
            break;
            case TimeUnit.Quarters:
                rv.Unit = TimeUnit.Quarters;
                rv.Count = timeSpan.TotalDays / 91;
            break;
            case TimeUnit.Months:
                rv.Unit = TimeUnit.Months;
                rv.Count = timeSpan.TotalDays / 30.437;
            break;
            case TimeUnit.Weeks:
                rv.Unit = TimeUnit.Weeks;
                rv.Count = timeSpan.TotalDays / 7;
            break;
            case TimeUnit.Days:
                rv.Unit = TimeUnit.Days;
                rv.Count = timeSpan.TotalDays;
            break;
            case TimeUnit.Hours:
                rv.Unit = TimeUnit.Hours;
                rv.Count = timeSpan.TotalHours;
            break;
            case TimeUnit.Minutes:
                rv.Unit = TimeUnit.Minutes;
                rv.Count = timeSpan.TotalMinutes;
            break;
            default: //intentional fall-through
            case TimeUnit.Seconds:
                rv.Unit = TimeUnit.Seconds;
                rv.Count = timeSpan.TotalSeconds;
            break;
            case TimeUnit.Milliseconds:
                rv.Unit = TimeUnit.Milliseconds;
                rv.Count = timeSpan.TotalMilliseconds;
            break;
            case TimeUnit.Microseconds:
                rv.Unit = TimeUnit.Microseconds;
                rv.Count = timeSpan.TotalMicroseconds;
            break;
            case TimeUnit.Nanoseconds:
                rv.Unit = TimeUnit.Nanoseconds;
                rv.Count = timeSpan.TotalNanoseconds;
            break;
        }

        return rv;
    }
}
