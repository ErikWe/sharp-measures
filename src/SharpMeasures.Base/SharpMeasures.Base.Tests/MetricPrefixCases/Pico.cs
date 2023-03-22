namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Pico
{
    private static MetricPrefix Target() => MetricPrefix.Pico;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, -12);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
