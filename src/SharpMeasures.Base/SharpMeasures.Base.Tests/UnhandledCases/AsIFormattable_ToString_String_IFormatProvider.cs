namespace SharpMeasures.Tests.UnhandledCases;

using System;
using System.Globalization;

using Xunit;

public class AsIFormattable_ToString_String_IFormatProvider
{
    private static string Target(Unhandled unhandled, string? format, IFormatProvider? formatProvider)
    {
        return execute(unhandled);

        string execute(IFormattable formattable) => formattable.ToString(format, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void General_MatchToString(Unhandled unhandled)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(unhandled, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void FloatingPoint_MatchToString(Unhandled unhandled)
    {
        var format = "F4";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(unhandled, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void NullString_MatchToString(Unhandled unhandled)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        MatchToString(unhandled, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void NullIFormatProvider_MatchToString(Unhandled unhandled)
    {
        var format = "G";
        IFormatProvider? provider = null;

        MatchToString(unhandled, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void NullStringAndIFormatProvider_MatchToString(Unhandled unhandled)
    {
        string? format = null;
        IFormatProvider? provider = null;

        MatchToString(unhandled, format, provider);
    }

    private static void MatchToString(Unhandled unhandled, string? format, IFormatProvider? formatProvider)
    {
        var expected = unhandled.ToString(format, formatProvider);

        var actual = Target(unhandled, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
