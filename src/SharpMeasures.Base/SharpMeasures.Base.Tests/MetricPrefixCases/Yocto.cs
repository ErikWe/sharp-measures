namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Yocto
{
    private static MetricPrefix Target() => MetricPrefix.Yocto;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, -24);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
