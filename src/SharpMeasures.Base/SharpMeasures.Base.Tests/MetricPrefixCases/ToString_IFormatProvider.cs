namespace SharpMeasures.Tests.MetricPrefixCases;

using System;
using System.Globalization;

using Xunit;

public class ToString_IFormatProvider
{
    private static string Target(MetricPrefix prefix, IFormatProvider? formatProvider) => prefix.ToString(formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void Valid_MatchFactorToString(MetricPrefix prefix)
    {
        var formatProvider = CultureInfo.CurrentCulture;

        MatchFactorToString(prefix, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void Null_MatchFactorToString(MetricPrefix prefix)
    {
        IFormatProvider? formatProvider = null;

        MatchFactorToString(prefix, formatProvider);
    }

    private static void MatchFactorToString(MetricPrefix prefix, IFormatProvider? formatProvider)
    {
        var expected = prefix.Factor.ToString(formatProvider);

        var actual = Target(prefix, formatProvider);

        Assert.Equal(expected, actual);
    }
}
