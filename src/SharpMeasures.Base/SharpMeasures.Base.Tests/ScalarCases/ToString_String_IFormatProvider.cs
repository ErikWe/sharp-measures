namespace SharpMeasures.Tests.ScalarCases;

using System;
using System.Globalization;

using Xunit;

public class ToString_String_IFormatProvider
{
    private static string Target(Scalar scalar, string? format, IFormatProvider? formatProvider) => scalar.ToString(format, formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void General_MatchToDoubleToString(Scalar scalar)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        MatchToDoubleToString(scalar, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void FloatingPoint_MatchToDoubleToString(Scalar scalar)
    {
        var format = "F4";
        var provider = CultureInfo.CurrentCulture;

        MatchToDoubleToString(scalar, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void NullString_MatchToDoubleToString(Scalar scalar)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        MatchToDoubleToString(scalar, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void NullIFormatProvider_MatchToDoubleToString(Scalar scalar)
    {
        var format = "G";
        IFormatProvider? provider = null;

        MatchToDoubleToString(scalar, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void NullStringAndIFormatProvider_MatchToDoubleToString(Scalar scalar)
    {
        string? format = null;
        IFormatProvider? provider = null;

        MatchToDoubleToString(scalar, format, provider);
    }

    private static void MatchToDoubleToString(Scalar scalar, string? format, IFormatProvider? formatProvider)
    {
        var expected = scalar.ToDouble().ToString(format, formatProvider);

        var actual = Target(scalar, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
