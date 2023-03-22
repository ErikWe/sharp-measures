namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Deca
{
    private static MetricPrefix Target() => MetricPrefix.Deca;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, 1);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
