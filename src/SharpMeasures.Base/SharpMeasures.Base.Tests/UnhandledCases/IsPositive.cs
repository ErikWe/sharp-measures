namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class IsPositive
{
    private static bool Target(Unhandled unhandled) => unhandled.IsPositive;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeIsPositive(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.IsPositive;

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
