namespace SharpMeasures.Tests.Unhandled4Cases;

using System;
using System.Globalization;

using Xunit;

public class AsIFormattable_ToString_String_IFormatProvider
{
    private static string Target(Unhandled4 vector, string? format, IFormatProvider? formatProvider)
    {
        return execute(vector);

        string execute(IFormattable formattable) => formattable.ToString(format, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void General_MatchToString(Unhandled4 vector)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void FloatingPoint_MatchToString(Unhandled4 vector)
    {
        var format = "({0:F4}, {1:F3}, {2:F5}, {3:G})";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void NullString_MatchToString(Unhandled4 vector)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        MatchToString(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void NullIFormatProvider_MatchToString(Unhandled4 vector)
    {
        var format = "G";
        IFormatProvider? provider = null;

        MatchToString(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void NullStringAndIFormatProvider_MatchToString(Unhandled4 vector)
    {
        string? format = null;
        IFormatProvider? provider = null;

        MatchToString(vector, format, provider);
    }

    private static void MatchToString(Unhandled4 vector, string? format, IFormatProvider? formatProvider)
    {
        var expected = vector.ToString(format, formatProvider);

        var actual = Target(vector, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
