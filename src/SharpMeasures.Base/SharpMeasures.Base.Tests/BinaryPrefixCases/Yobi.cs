namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class Yobi
{
    private static BinaryPrefix Target() => BinaryPrefix.Yobi;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(2, 80);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
