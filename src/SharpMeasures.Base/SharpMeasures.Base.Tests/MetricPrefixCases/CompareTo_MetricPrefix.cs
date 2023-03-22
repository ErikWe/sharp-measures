namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class CompareTo_MetricPrefix
{
    private static int Target(MetricPrefix prefix, MetricPrefix other) => prefix.CompareTo(other);

    [Fact]
    public void Null_One()
    {
        var prefix = Datasets.GetValidMetricPrefix();
        var other = Datasets.GetNullMetricPrefix();

        var actual = Target(prefix, other);

        Assert.Equal(1, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix_ValidMetricPrefix))]
    public void Valid_MatchSignOfFactorompareTo(MetricPrefix prefix, MetricPrefix other)
    {
        var expected = Math.Sign(prefix.Factor.CompareTo(other.Factor));

        var actual = Math.Sign(Target(prefix, other));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix_ValidMetricPrefix))]
    public void Valid_OneOfAllowedValues(MetricPrefix prefix, MetricPrefix other)
    {
        var allowed = new[] { 1, 0, -1 };

        var actual = Target(prefix, other);

        Assert.Contains(actual, allowed);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void SameInstance_Zero(MetricPrefix prefix)
    {
        var actual = Target(prefix, prefix);

        Assert.Equal(0, actual);
    }
}
