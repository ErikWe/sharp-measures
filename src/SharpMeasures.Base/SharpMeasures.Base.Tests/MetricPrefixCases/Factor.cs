namespace SharpMeasures.Tests.MetricPrefixCases;

using Xunit;

public class Factor
{
    private static Scalar Target(MetricPrefix prefix) => prefix.Factor;

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void Valid_NoException(MetricPrefix prefix)
    {
        var exception = Record.Exception(() => Target(prefix));

        Assert.Null(exception);
    }
}
