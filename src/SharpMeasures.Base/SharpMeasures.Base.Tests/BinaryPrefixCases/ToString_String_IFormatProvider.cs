namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;
using System.Globalization;

using Xunit;

public class ToString_String_IFormatProvider
{
    private static string Target(BinaryPrefix prefix, string? format, IFormatProvider? formatProvider) => prefix.ToString(format, formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void General_MatchFactorToString(BinaryPrefix prefix)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        MatchFactorToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void FloatingPoint_MatchFactorToString(BinaryPrefix prefix)
    {
        var format = "F4";
        var provider = CultureInfo.CurrentCulture;

        MatchFactorToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void NullString_MatchFactorToString(BinaryPrefix prefix)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        MatchFactorToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void NullIFormatProvider_MatchFactorToString(BinaryPrefix prefix)
    {
        var format = "G";
        IFormatProvider? provider = null;

        MatchFactorToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void NullStringAndIFormatProvider_MatchFactorToString(BinaryPrefix prefix)
    {
        string? format = null;
        IFormatProvider? provider = null;

        MatchFactorToString(prefix, format, provider);
    }

    private static void MatchFactorToString(BinaryPrefix prefix, string? format, IFormatProvider? formatProvider)
    {
        var expected = prefix.Factor.ToString(format, formatProvider);

        var actual = Target(prefix, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
