namespace SharpMeasures.Tests.Unhandled3Cases;

using System;
using System.Globalization;

using Xunit;

public class ToString_IFormatProvider
{
    private static string Target(Unhandled3 vector, IFormatProvider? formatProvider) => vector.ToString(formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void Valid_MatchCustom(Unhandled3 vector)
    {
        var formatProvider = CultureInfo.CurrentCulture;

        MatchCustom(vector, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void Null_MatchCustom(Unhandled3 vector)
    {
        IFormatProvider? formatProvider = null;

        MatchCustom(vector, formatProvider);
    }

    private static void MatchCustom(Unhandled3 vector, IFormatProvider? formatProvider)
    {
        var expected = string.Format(formatProvider, "({0}, {1}, {2})", vector.X, vector.Y, vector.Z);

        var actual = Target(vector, formatProvider);

        Assert.Equal(expected, actual);
    }
}
