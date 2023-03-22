namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class NaN
{
    private static Unhandled Target() => Unhandled.NaN;

    [Fact]
    public void MagnitudeIsNaN()
    {
        var actual = Target().Magnitude;

        Assert.Equal(Scalar.NaN, actual);
    }
}
