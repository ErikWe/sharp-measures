namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class IsZero
{
    private static bool Target(Unhandled unhandled) => unhandled.IsZero;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeIsZero(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.IsZero;

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
