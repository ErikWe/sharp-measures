namespace SharpMeasures.Tests.MetricPrefixCases;

using System;
using System.Globalization;

using Xunit;

public class ToString_String_IFormatProvider
{
    private static string Target(MetricPrefix prefix, string? format, IFormatProvider? formatProvider) => prefix.ToString(format, formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void General_MatchFactorToString(MetricPrefix prefix)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        MatchFactorToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void FloatingPoint_MatchFactorToString(MetricPrefix prefix)
    {
        var format = "F4";
        var provider = CultureInfo.CurrentCulture;

        MatchFactorToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void NullString_MatchFactorToString(MetricPrefix prefix)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        MatchFactorToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void NullIFormatProvider_MatchFactorToString(MetricPrefix prefix)
    {
        var format = "G";
        IFormatProvider? provider = null;

        MatchFactorToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void NullStringAndIFormatProvider_MatchFactorToString(MetricPrefix prefix)
    {
        string? format = null;
        IFormatProvider? provider = null;

        MatchFactorToString(prefix, format, provider);
    }

    private static void MatchFactorToString(MetricPrefix prefix, string? format, IFormatProvider? formatProvider)
    {
        var expected = prefix.Factor.ToString(format, formatProvider);

        var actual = Target(prefix, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
