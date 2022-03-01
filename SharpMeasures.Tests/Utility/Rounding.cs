namespace ErikWe.SharpMeasures.Tests.Utility;

using System;

public static class Rounding
{
    public static double ToSignificantDigits(double value, int digits)
    {
        if (value == 0)
        {
            return 0;
        }

        double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(value))) + 1);
        return scale * Math.Round(value / scale, digits);
    }
}
