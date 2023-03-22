namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Nano
{
    private static MetricPrefix Target() => MetricPrefix.Nano;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, -9);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
