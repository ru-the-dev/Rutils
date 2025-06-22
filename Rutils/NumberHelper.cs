using Rutils.Data;

namespace Rutils;

public static class NumberUtils
{
    public static NumberScale ReduceScale(IEnumerable<double> numbers)
    {
        double val = numbers.Max() - Math.Min(numbers.Min(), 0d);

        if (val > 1_000_000_000_000d)
        {
            return NumberScale.Trillions;
        }
        else if (val > 1_000_000_000d)
        {
            return NumberScale.Billions;
        }
        else if (val > 1_000_000d)
        {
            return NumberScale.Millions;
        }
        else if (val > 1_000d)
        {
            return NumberScale.Thousands;
        }
        else
        {
            return NumberScale.Units;
        }

    } 

    public static double ScaleToRange(double number, double originalMin, double originalMax, double newMin = -1d, double newMax = 1d)
    {
        return (number - originalMin) / (originalMax - originalMin) * (newMax - newMin) + newMin;
    }

    public static float ScaleToRange(float number, float originalMin, float originalMax, float newMin = -1f, float newMax = 1f)
    {
        return (float)ScaleToRange((double)number, (double)originalMin, (double)originalMax, (double)newMin, (double)newMax);
    }

    public static int ScaleToRange(int number, int originalMin, int originalMax, int newMin = -1, int newMax = 1)
    {
        return (int)ScaleToRange((double)number, (double)originalMin, (double)originalMax, (double)newMin, (double)newMax);
    }
    
    public static float ToRadians(float nr)
    {
        return (float)ToRadians((double)nr);
    }

    public static double ToRadians(double nr)
    {
        return Math.PI / 180d * nr;
    }
}
