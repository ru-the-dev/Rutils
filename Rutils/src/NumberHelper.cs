

using System.Numerics;

namespace Rutils;

public static class NumberHelper
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
}
