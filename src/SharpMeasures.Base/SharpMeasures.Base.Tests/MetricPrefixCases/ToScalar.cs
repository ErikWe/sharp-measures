namespace SharpMeasures.Tests.MetricPrefixCases;

using Xunit;

public class ToScalar
{
    private static Scalar Target(MetricPrefix prefix) => prefix.ToScalar();

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void Valid_MatchFactor(MetricPrefix prefix)
    {
        var expected = prefix.Factor;

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }
}
