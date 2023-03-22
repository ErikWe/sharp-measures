namespace SharpMeasures.Tests.BinaryPrefixCases;

using Xunit;

public class Zero
{
    private static BinaryPrefix Target() => BinaryPrefix.Zero;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = 0;

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
