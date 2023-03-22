namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class IsFinite
{
    private static bool Target(Unhandled unhandled) => unhandled.IsFinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeIsFinite(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.IsFinite;

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
