namespace SharpMeasures.Tests.MetricPrefixCases;

using Xunit;

public class Equals_Object
{
    private static bool Target(MetricPrefix prefix, object? other) => prefix.Equals(other);

    [Fact]
    public void Null_False()
    {
        var prefix = Datasets.GetValidMetricPrefix();
        object? other = null;

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Fact]
    public void DifferentType_False()
    {
        var prefix = Datasets.GetValidMetricPrefix();
        var other = string.Empty;

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix_ValidMetricPrefix))]
    public void SameType_MatchMetricPrefixEquals(MetricPrefix prefix, MetricPrefix other)
    {
        var expected = prefix.Equals(other);

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
