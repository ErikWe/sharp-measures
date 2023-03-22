namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Peta
{
    private static MetricPrefix Target() => MetricPrefix.Peta;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, 15);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
