namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class IsNaN
{
    private static bool Target(Unhandled unhandled) => unhandled.IsNaN;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeIsNaN(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.IsNaN;

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
