namespace SharpMeasures.Tests.MetricPrefixCases;

using Xunit;

public class Operator_Inequality
{
    private static bool Target(MetricPrefix lhs, MetricPrefix rhs) => lhs != rhs;

    [Fact]
    public void NullLHS_True()
    {
        var lhs = Datasets.GetNullMetricPrefix();
        var rhs = Datasets.GetValidMetricPrefix();

        var actual = Target(lhs, rhs);

        Assert.True(actual);
    }

    [Fact]
    public void NullRHS_True()
    {
        var lhs = Datasets.GetValidMetricPrefix();
        var rhs = Datasets.GetNullMetricPrefix();

        var actual = Target(lhs, rhs);

        Assert.True(actual);
    }

    [Fact]
    public void NullLHSAndRHS_False()
    {
        var lhs = Datasets.GetNullMetricPrefix();
        var rhs = Datasets.GetNullMetricPrefix();

        var actual = Target(lhs, rhs);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix_ValidMetricPrefix))]
    public void Valid_OppositeOfEqualsMethod(MetricPrefix lhs, MetricPrefix rhs)
    {
        var expected = lhs.Equals(rhs) is false;

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void SameInstance_False(MetricPrefix prefix)
    {
        var actual = Target(prefix, prefix);

        Assert.False(actual);
    }
}
