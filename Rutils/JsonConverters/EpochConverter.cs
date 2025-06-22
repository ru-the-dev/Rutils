using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Rutils.Data;

namespace Rutils.JsonConverters;

public abstract class EpochConverter : JsonConverter<DateTime>
{
    private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public TimeUnit TimeUnit { get; }
    public double MillisecondMultiplier { get; }

    protected EpochConverter(TimeUnit timeUnit)
    {
        TimeUnit = timeUnit;
        MillisecondMultiplier = timeUnit switch
        {
            TimeUnit.Years => 31_450_000_000,
            TimeUnit.Months => 2_628_000_000,
            TimeUnit.Weeks => 604_800_000,
            TimeUnit.Days => 86_400_000,
            TimeUnit.Hours => 3_600_000,
            TimeUnit.Minutes => 60_000,
            TimeUnit.Seconds => 1000,
            TimeUnit.Milliseconds => 1,
            TimeUnit.Microseconds => 1d / 1_000,
            TimeUnit.Nanoseconds => 1d / 1_000_000,
            _ => throw new ArgumentOutOfRangeException(nameof(timeUnit), "Unsupported TimeUnit")
        };
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        double value = reader.GetDouble();
        // Convert to milliseconds for DateTimeOffset.FromUnixTimeMilliseconds
        long epochMilliseconds = (long)(value * MillisecondMultiplier);
        return DateTimeOffset.FromUnixTimeMilliseconds(epochMilliseconds).UtcDateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var elapsedMilliseconds = (value.ToUniversalTime() - Epoch).TotalMilliseconds;
        // Scale by inverse multiplier to get the original unit value:
        double convertedValue = elapsedMilliseconds / MillisecondMultiplier;

        // Write as raw double value
        writer.WriteNumberValue(convertedValue);
    }
}

public class MicrosecondEpochConverter : EpochConverter
{
    public MicrosecondEpochConverter() : base(TimeUnit.Microseconds) { }
}

public class MillisecondEpochConverter : EpochConverter
{
    public MillisecondEpochConverter() : base(TimeUnit.Milliseconds) { }
}

public class NanosecondEpochConverter : EpochConverter
{
    public NanosecondEpochConverter() : base(TimeUnit.Nanoseconds) { }
}

public class SecondEpochConverter : EpochConverter
{
    public SecondEpochConverter() : base(TimeUnit.Seconds) { }
}
