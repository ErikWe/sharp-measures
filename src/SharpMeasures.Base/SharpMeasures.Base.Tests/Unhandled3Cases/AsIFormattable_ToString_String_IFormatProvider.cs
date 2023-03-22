namespace SharpMeasures.Tests.Unhandled3Cases;

using System;
using System.Globalization;

using Xunit;

public class AsIFormattable_ToString_String_IFormatProvider
{
    private static string Target(Unhandled3 vector, string? format, IFormatProvider? formatProvider)
    {
        return execute(vector);

        string execute(IFormattable formattable) => formattable.ToString(format, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void General_MatchToString(Unhandled3 vector)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void FloatingPoint_MatchToString(Unhandled3 vector)
    {
        var format = "({0:F4}, {1:F3}, {2:F5})";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void NullString_MatchToString(Unhandled3 vector)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        MatchToString(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void NullIFormatProvider_MatchToString(Unhandled3 vector)
    {
        var format = "G";
        IFormatProvider? provider = null;

        MatchToString(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void NullStringAndIFormatProvider_MatchToString(Unhandled3 vector)
    {
        string? format = null;
        IFormatProvider? provider = null;

        MatchToString(vector, format, provider);
    }

    private static void MatchToString(Unhandled3 vector, string? format, IFormatProvider? formatProvider)
    {
        var expected = vector.ToString(format, formatProvider);

        var actual = Target(vector, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
