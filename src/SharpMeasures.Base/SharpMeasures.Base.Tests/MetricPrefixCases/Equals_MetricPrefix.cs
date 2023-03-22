namespace SharpMeasures.Tests.MetricPrefixCases;

using Xunit;

public class Equals_MetricPrefix
{
    private static bool Target(MetricPrefix prefix, MetricPrefix other) => prefix.Equals(other);

    [Fact]
    public void Null_False()
    {
        var prefix = Datasets.GetValidMetricPrefix();
        var other = Datasets.GetNullMetricPrefix();

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix_ValidMetricPrefix))]
    public void Valid_MatchFactorEquals(MetricPrefix prefix, MetricPrefix other)
    {
        var expected = prefix.Factor.Equals(other.Factor);

        var actual = Target(prefix, other);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void SameInstance_True(MetricPrefix prefix)
    {
        var actual = Target(prefix, prefix);

        Assert.True(actual);
    }
}
