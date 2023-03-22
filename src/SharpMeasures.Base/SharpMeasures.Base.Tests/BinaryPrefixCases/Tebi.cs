namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class Tebi
{
    private static BinaryPrefix Target() => BinaryPrefix.Tebi;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(2, 40);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
