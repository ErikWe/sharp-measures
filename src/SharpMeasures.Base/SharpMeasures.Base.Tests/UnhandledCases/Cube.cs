namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Cube
{
    private static Unhandled Target(Unhandled unhandled) => unhandled.Cube();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeCube(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.Cube();

        var actual = Target(unhandled).Magnitude;

        Assert.Equal(expected, actual);
    }
}
