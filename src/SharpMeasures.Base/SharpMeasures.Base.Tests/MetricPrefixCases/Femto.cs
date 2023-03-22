namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Femto
{
    private static MetricPrefix Target() => MetricPrefix.Femto;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, -15);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
