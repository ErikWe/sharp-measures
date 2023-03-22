namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class Kibi
{
    private static BinaryPrefix Target() => BinaryPrefix.Kibi;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = Math.Pow(2, 10);

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
