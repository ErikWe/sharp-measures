namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Atto
{
    private static MetricPrefix Target() => MetricPrefix.Atto;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, -18);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
