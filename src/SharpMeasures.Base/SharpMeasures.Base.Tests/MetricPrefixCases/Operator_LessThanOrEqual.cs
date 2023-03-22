namespace SharpMeasures.Tests.MetricPrefixCases;

using Xunit;

public class Operator_LessThanOrEqual
{
    private static bool Target(MetricPrefix lhs, MetricPrefix rhs) => lhs <= rhs;

    [Fact]
    public void NullLHS_False()
    {
        var prefix = Datasets.GetNullMetricPrefix();
        var other = Datasets.GetValidMetricPrefix();

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Fact]
    public void NullRHS_False()
    {
        var prefix = Datasets.GetValidMetricPrefix();
        var other = Datasets.GetNullMetricPrefix();

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Fact]
    public void NullLHSAndRHS_False()
    {
        var prefix = Datasets.GetNullMetricPrefix();
        var other = Datasets.GetNullMetricPrefix();

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix_ValidMetricPrefix))]
    public void Valid_MatchFactorLessThanOrEqual(MetricPrefix lhs, MetricPrefix rhs)
    {
        var expected = lhs.Factor <= rhs.Factor;

        var actual = Target(lhs, rhs);

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
