namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class IsNegativeInfinity
{
    private static bool Target(Unhandled unhandled) => unhandled.IsNegativeInfinity;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeIsNegativeInfinity(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.IsNegativeInfinity;

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
