namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Ronna
{
    private static MetricPrefix Target() => MetricPrefix.Ronna;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, 27);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
