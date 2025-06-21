namespace Rutils;

public enum NumberScale
{
    Units,
    Thousands,
    Millions,
    Billions,
    Trillions
}


public static class NumberScaleExtensions
{
    public static double GetScaleFactor(this NumberScale scale)
    {
        switch (scale)
        {
            case NumberScale.Units:
                return 1;
            case NumberScale.Thousands:
                return 1d / 1_000d;
            case NumberScale.Millions:
                return 1d / 1_000_000d;
            case NumberScale.Billions:
                return 1d / 1_000_000_000d;
            case NumberScale.Trillions:
                return 1d / 1_000_000_000_000d;
            default:
                throw new NotImplementedException();
        }
    }
}




