namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class NegativeInfinity
{
    private static Unhandled Target() => Unhandled.NegativeInfinity;

    [Fact]
    public void MagnitudeIsNegativeInfinty()
    {
        var actual = Target().Magnitude;

        Assert.Equal(Scalar.NegativeInfinity, actual);
    }
}
