namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;
using System.Globalization;

using Xunit;

public class AsIFormattable_ToString_String_IFormatProvider
{
    private static string Target(BinaryPrefix prefix, string? format, IFormatProvider? formatProvider)
    {
        return execute(prefix);

        string execute(IFormattable formattable) => formattable.ToString(format, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void General_MatchToString(BinaryPrefix prefix)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void FloatingPoint_MatchToString(BinaryPrefix prefix)
    {
        var format = "F4";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void NullString_MatchToString(BinaryPrefix prefix)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        MatchToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void NullIFormatProvider_MatchToString(BinaryPrefix prefix)
    {
        var format = "G";
        IFormatProvider? provider = null;

        MatchToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void NullStringAndIFormatProvider_MatchToString(BinaryPrefix prefix)
    {
        string? format = null;
        IFormatProvider? provider = null;

        MatchToString(prefix, format, provider);
    }

    private static void MatchToString(BinaryPrefix prefix, string? format, IFormatProvider? formatProvider)
    {
        var expected = prefix.ToString(format, formatProvider);

        var actual = Target(prefix, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
