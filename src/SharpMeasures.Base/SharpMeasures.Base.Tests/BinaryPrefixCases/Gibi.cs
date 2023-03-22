namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class Gibi
{
    private static BinaryPrefix Target() => BinaryPrefix.Gibi;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(2, 30);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
