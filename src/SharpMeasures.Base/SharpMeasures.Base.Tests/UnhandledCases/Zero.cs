namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Zero
{
    private static Unhandled Target() => Unhandled.Zero;

    [Fact]
    public void MagnitudeIsZero()
    {
        var actual = Target().Magnitude;

        Assert.Equal(0, actual);
    }
}
