namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Yotta
{
    private static MetricPrefix Target() => MetricPrefix.Yotta;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, 24);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
