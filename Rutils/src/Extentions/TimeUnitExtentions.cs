namespace Rutils;

public static class TimeUnitExtentions
{
    public static TimeSpan ToSpan(this TimeUnit timeUnit, double unitCount = 1)
    {
        switch(timeUnit)
        {
            
            default: //intentional fall-through
            case TimeUnit.Minutes:
                return TimeSpan.FromMinutes(unitCount);
            case TimeUnit.Hours:
                return TimeSpan.FromHours(unitCount);
            case TimeUnit.Days:
                return TimeSpan.FromDays(unitCount);
            case TimeUnit.Weeks:
                return TimeSpan.FromDays(unitCount * 7);
            case TimeUnit.Months:
                return TimeSpan.FromDays(unitCount * 30); //TODO: note this might cause bugs since this makes the assumption all months are 30 days long.
            case TimeUnit.Quarters: 
                return TimeSpan.FromDays(unitCount * 91);
            case TimeUnit.Years:
                return TimeSpan.FromDays(unitCount * 365);
        }
    }
}

public static class TimeUnitCountExtentions
{
    public static TimeSpan ToSpan(this TimeUnitCount timeUnit)
    {
        return timeUnit.Unit.ToSpan(timeUnit.Count);
    }
}