namespace SharpMeasures.Tests.MetricPrefixCases;

using System;
using System.Globalization;

using Xunit;

public class AsIFormattable_ToString_String_IFormatProvider
{
    private static string Target(MetricPrefix prefix, string? format, IFormatProvider? formatProvider)
    {
        return execute(prefix);

        string execute(IFormattable formattable) => formattable.ToString(format, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void General_MatchToString(MetricPrefix prefix)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void FloatingPoint_MatchToString(MetricPrefix prefix)
    {
        var format = "F4";
        var provider = CultureInfo.CurrentCulture;

        MatchToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void NullString_MatchToString(MetricPrefix prefix)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        MatchToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void NullIFormatProvider_MatchToString(MetricPrefix prefix)
    {
        var format = "G";
        IFormatProvider? provider = null;

        MatchToString(prefix, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void NullStringAndIFormatProvider_MatchToString(MetricPrefix prefix)
    {
        string? format = null;
        IFormatProvider? provider = null;

        MatchToString(prefix, format, provider);
    }

    private static void MatchToString(MetricPrefix prefix, string? format, IFormatProvider? formatProvider)
    {
        var expected = prefix.ToString(format, formatProvider);

        var actual = Target(prefix, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
