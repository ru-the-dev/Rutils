namespace Rutils;

public enum TimeUnit
{
    Years,
    Months, 
    Weeks, 
    Days,
    Hours, 
    Minutes, 
    Seconds, 
    Milliseconds, 
    Nanoseconds, 
    Microseconds
}

public struct TimeUnitCount
{
    public TimeUnit unit;
    public double count;

    public override string ToString()
    {
        return $"{Math.Abs(count)} {ToString(unit)}";
    }  

    public static string ToString(TimeUnit unit)
    {
        return Enum.GetName(unit)!.ToLower();
    }

}