namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Zepto
{
    private static MetricPrefix Target() => MetricPrefix.Zepto;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, -21);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
