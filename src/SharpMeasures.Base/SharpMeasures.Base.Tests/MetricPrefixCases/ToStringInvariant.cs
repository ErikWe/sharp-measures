namespace SharpMeasures.Tests.MetricPrefixCases;

using Xunit;

public class ToStringInvariant
{
    private static string Target(MetricPrefix prefix) => prefix.ToStringInvariant();

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void MatchFactorToStringInvariant(MetricPrefix prefix)
    {
        var expected = prefix.Factor.ToStringInvariant();

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }
}
