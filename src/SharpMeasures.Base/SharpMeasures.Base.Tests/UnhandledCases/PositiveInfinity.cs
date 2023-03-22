namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class PositiveInfinty
{
    private static Unhandled Target() => Unhandled.PositiveInfinity;

    [Fact]
    public void MagnitudeIsPositiveInfinty()
    {
        var actual = Target().Magnitude;

        Assert.Equal(Scalar.PositiveInfinity, actual);
    }
}
