using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Rutils;


public class MicrosecondEpochConverter : EpochConverter
{
    public MicrosecondEpochConverter() : base(TimeUnit.Microseconds) {}
}

public class MillisecondEpochConverter : EpochConverter
{
    public MillisecondEpochConverter() : base(TimeUnit.Milliseconds) {}
}

public class NanosecondEpochConverter : EpochConverter
{
    public NanosecondEpochConverter() : base(TimeUnit.Nanoseconds) {}
}

public abstract class EpochConverter : DateTimeConverterBase
{
    private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public TimeUnit TimeUnit { get; }

    public double MillisecondMultiplier { get; }

    public EpochConverter(TimeUnit timeUnit)
    {
        TimeUnit = timeUnit;

        switch (TimeUnit)
        {
            case TimeUnit.Years:
                MillisecondMultiplier = 31_450_000_000;
            break;
            case TimeUnit.Months:
                MillisecondMultiplier = 2_628_000_000;
            break;
            case TimeUnit.Weeks:
                MillisecondMultiplier = 604_800_000;
            break;
            case TimeUnit.Days:
                MillisecondMultiplier = 86_400_000;
            break;
            case TimeUnit.Hours:
                MillisecondMultiplier = 3_600_000;
            break;
            case TimeUnit.Minutes:
                MillisecondMultiplier = 60_000;
            break;
            case TimeUnit.Seconds:
                MillisecondMultiplier = 1000;
            break;
            case TimeUnit.Milliseconds:
                MillisecondMultiplier = 1;
            break;
            case TimeUnit.Microseconds:
                MillisecondMultiplier = 1d / 1_000;
            break;
            case TimeUnit.Nanoseconds:
                MillisecondMultiplier = 1d / 1_000_000;
            break;
        }
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        writer.WriteRawValue(((DateTime)value! - _epoch).TotalMilliseconds + new string('0', (int)Math.Log10(MillisecondMultiplier)));
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        long epochMilliseconds = (long)(Convert.ToDouble(reader.Value!) * MillisecondMultiplier);
        return DateTimeOffset.FromUnixTimeMilliseconds(epochMilliseconds).DateTime;
    }
}
