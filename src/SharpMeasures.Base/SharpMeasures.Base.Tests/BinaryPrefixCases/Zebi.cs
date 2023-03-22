namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class Zebi
{
    private static BinaryPrefix Target() => BinaryPrefix.Zebi;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(2, 70);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
