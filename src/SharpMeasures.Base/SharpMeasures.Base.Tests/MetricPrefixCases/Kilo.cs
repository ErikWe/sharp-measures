namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Kilo
{
    private static MetricPrefix Target() => MetricPrefix.Kilo;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, 3);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
