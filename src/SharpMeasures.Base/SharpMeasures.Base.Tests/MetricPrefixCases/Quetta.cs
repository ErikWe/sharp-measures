namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Quetta
{
    private static MetricPrefix Target() => MetricPrefix.Quetta;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, 30);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
