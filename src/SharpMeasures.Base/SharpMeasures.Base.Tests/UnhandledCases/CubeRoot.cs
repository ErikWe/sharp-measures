namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class CubeRoot
{
    private static Unhandled Target(Unhandled unhandled) => unhandled.CubeRoot();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeCubeRoot(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.CubeRoot();

        var actual = Target(unhandled).Magnitude;

        Assert.Equal(expected, actual);
    }
}
