namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class IsPositiveInfinity
{
    private static bool Target(Unhandled unhandled) => unhandled.IsPositiveInfinity;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeIsPositiveInfinity(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.IsPositiveInfinity;

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
