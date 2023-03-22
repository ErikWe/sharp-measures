namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Quecto
{
    private static MetricPrefix Target() => MetricPrefix.Quecto;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, -30);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
