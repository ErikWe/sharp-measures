namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class Identity
{
    private static BinaryPrefix Target() => BinaryPrefix.Identity;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(2, 0);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
