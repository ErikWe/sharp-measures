namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Micro
{
    private static MetricPrefix Target() => MetricPrefix.Micro;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, -6);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
