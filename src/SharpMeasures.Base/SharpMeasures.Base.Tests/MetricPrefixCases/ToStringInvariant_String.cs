namespace SharpMeasures.Tests.MetricPrefixCases;

using System.Globalization;

using Xunit;

public class ToStringInvariant_String
{
    private static string Target(MetricPrefix prefix, string? format) => prefix.ToStringInvariant(format);

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void General_MatchFactorToStringInvariant(MetricPrefix prefix)
    {
        var format = "G";

        MatchFactorToStringInvariant(prefix, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void FloatingPoint_MatchFactorToStringInvariant(MetricPrefix prefix)
    {
        var format = "F4";

        MatchFactorToStringInvariant(prefix, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void Null_MatchFactorToStringInvariant(MetricPrefix prefix)
    {
        string? format = null;

        MatchFactorToStringInvariant(prefix, format);
    }

    private static void MatchFactorToStringInvariant(MetricPrefix prefix, string? format)
    {
        var expected = prefix.Factor.ToString(format, CultureInfo.InvariantCulture);

        var actual = Target(prefix, format);

        Assert.Equal(expected, actual);
    }
}
