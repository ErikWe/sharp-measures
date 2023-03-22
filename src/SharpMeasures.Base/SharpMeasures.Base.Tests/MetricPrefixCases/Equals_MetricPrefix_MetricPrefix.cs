namespace SharpMeasures.Tests.MetricPrefixCases;

using Xunit;

public class Equals_MetricPrefix_MetricPrefix
{
    private static bool Target(MetricPrefix lhs, MetricPrefix rhs) => MetricPrefix.Equals(lhs, rhs);

    [Fact]
    public void NullLHS_False()
    {
        var lhs = Datasets.GetNullMetricPrefix();
        var rhs = Datasets.GetValidMetricPrefix();

        var actual = Target(lhs, rhs);

        Assert.False(actual);
    }

    [Fact]
    public void NullRHS_False()
    {
        var lhs = Datasets.GetValidMetricPrefix();
        var rhs = Datasets.GetNullMetricPrefix();

        var actual = Target(lhs, rhs);

        Assert.False(actual);
    }

    [Fact]
    public void NullLHSAndRHS_True()
    {
        var lhs = Datasets.GetNullMetricPrefix();
        var rhs = Datasets.GetNullMetricPrefix();

        var actual = Target(lhs, rhs);

        Assert.True(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix_ValidMetricPrefix))]
    public void Valid_MatchInstanceMethod(MetricPrefix lhs, MetricPrefix rhs)
    {
        var expected = lhs.Equals(rhs);

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
