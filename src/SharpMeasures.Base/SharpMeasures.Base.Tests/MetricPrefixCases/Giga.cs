namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Giga
{
    private static MetricPrefix Target() => MetricPrefix.Giga;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, 9);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
