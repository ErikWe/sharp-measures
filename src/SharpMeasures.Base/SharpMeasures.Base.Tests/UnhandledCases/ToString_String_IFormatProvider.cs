namespace SharpMeasures.Tests.UnhandledCases;

using System;
using System.Globalization;

using Xunit;

public class ToString_String_IFormatProvider
{
    private static string Target(Unhandled unhandled, string? format, IFormatProvider? formatProvider) => unhandled.ToString(format, formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void General_MatchMagnitudeToString(Unhandled unhandled)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        MatchMagnitudeToString(unhandled, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void FloatingPoint_MatchMagnitudeToString(Unhandled unhandled)
    {
        var format = "F4";
        var provider = CultureInfo.CurrentCulture;

        MatchMagnitudeToString(unhandled, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void NullString_MatchMagnitudeToString(Unhandled unhandled)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        MatchMagnitudeToString(unhandled, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void NullIFormatProvider_MatchMagnitudeToString(Unhandled unhandled)
    {
        var format = "G";
        IFormatProvider? provider = null;

        MatchMagnitudeToString(unhandled, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void NullStringAndIFormatProvider_MatchMagnitudeToString(Unhandled unhandled)
    {
        string? format = null;
        IFormatProvider? provider = null;

        MatchMagnitudeToString(unhandled, format, provider);
    }

    private static void MatchMagnitudeToString(Unhandled unhandled, string? format, IFormatProvider? formatProvider)
    {
        var expected = unhandled.Magnitude.ToString(format, formatProvider);

        var actual = Target(unhandled, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
