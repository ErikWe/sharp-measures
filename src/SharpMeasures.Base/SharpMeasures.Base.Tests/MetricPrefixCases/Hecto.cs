namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Hecto
{
    private static MetricPrefix Target() => MetricPrefix.Hecto;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, 2);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
