namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Centi
{
    private static MetricPrefix Target() => MetricPrefix.Centi;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, -2);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
