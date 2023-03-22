namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Identity
{
    private static MetricPrefix Target() => MetricPrefix.Identity;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(10, 0);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
