namespace SharpMeasures.Tests.MetricPrefixCases;

using Xunit;

public class Zero
{
    private static MetricPrefix Target() => MetricPrefix.Zero;

    [Fact]
    public void MatchFactor()
    {
        Scalar expected = 0;

        var actual = Target().Factor;

        Assert.Equal(expected, actual);
    }
}
