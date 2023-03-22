namespace SharpMeasures.Tests.UnhandledCases;

using System;
using System.Globalization;

using Xunit;

public class ToString_IFormatProvider
{
    private static string Target(Unhandled unhandled, IFormatProvider? formatProvider) => unhandled.ToString(formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void Valid_MatchMagnitudeToString(Unhandled unhandled)
    {
        var formatProvider = CultureInfo.CurrentCulture;

        MatchMagnitudeToString(unhandled, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void Null_MatchMagnitudeToString(Unhandled unhandled)
    {
        IFormatProvider? formatProvider = null;

        MatchMagnitudeToString(unhandled, formatProvider);
    }

    private static void MatchMagnitudeToString(Unhandled unhandled, IFormatProvider? formatProvider)
    {
        var expected = unhandled.Magnitude.ToString(formatProvider);

        var actual = Target(unhandled, formatProvider);

        Assert.Equal(expected, actual);
    }
}
