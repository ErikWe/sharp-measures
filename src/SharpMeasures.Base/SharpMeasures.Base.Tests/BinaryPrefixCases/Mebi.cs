namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class Mebi
{
    private static BinaryPrefix Target() => BinaryPrefix.Mebi;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(2, 20);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
