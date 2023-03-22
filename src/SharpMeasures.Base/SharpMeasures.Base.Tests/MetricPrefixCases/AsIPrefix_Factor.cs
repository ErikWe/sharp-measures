namespace SharpMeasures.Tests.MetricPrefixCases;

using Xunit;

public class AsIPrefix_Factor
{
    private static Scalar Target(MetricPrefix prefix)
    {
        return execute(prefix);

        static Scalar execute(IPrefix prefix) => prefix.Factor;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void Valid_MatchInstanceProperty(MetricPrefix prefix)
    {
        var expected = prefix.Factor;

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }
}
