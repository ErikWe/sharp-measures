namespace SharpMeasures.Tests.ScalarCases;

using System;
using System.Globalization;

using Xunit;

public class AsIFormattable_ToString_String_IFormatProvider
{
    private static string Target(Scalar scalar, string? format, IFormatProvider? formatProvider)
    {
        return execute(scalar);

        string execute(IFormattable formattable) => formattable.ToString(format, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void General_MatchToString(Scalar scalar)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(scalar, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void FloatingPoint_MatchToString(Scalar scalar)
    {
        var format = "F4";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(scalar, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void NullString_MatchToString(Scalar scalar)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        MatchToString(scalar, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void NullIFormatProvider_MatchToString(Scalar scalar)
    {
        var format = "G";
        IFormatProvider? provider = null;

        MatchToString(scalar, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void NullStringAndIFormatProvider_MatchToString(Scalar scalar)
    {
        string? format = null;
        IFormatProvider? provider = null;

        MatchToString(scalar, format, provider);
    }

    private static void MatchToString(Scalar scalar, string? format, IFormatProvider? formatProvider)
    {
        var expected = scalar.ToString(format, formatProvider);

        var actual = Target(scalar, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
