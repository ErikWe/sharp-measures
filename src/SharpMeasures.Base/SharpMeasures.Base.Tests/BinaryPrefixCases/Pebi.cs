namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class Pebi
{
    private static BinaryPrefix Target() => BinaryPrefix.Pebi;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(2, 50);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
