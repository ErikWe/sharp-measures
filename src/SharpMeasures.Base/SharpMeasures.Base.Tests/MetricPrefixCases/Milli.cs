namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Milli
{
    private static MetricPrefix Target() => MetricPrefix.Milli;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, -3);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
