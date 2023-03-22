namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class Exbi
{
    private static BinaryPrefix Target() => BinaryPrefix.Exbi;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(2, 60);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
