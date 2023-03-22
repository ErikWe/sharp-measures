namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class IsNegative
{
    private static bool Target(Unhandled unhandled) => unhandled.IsNegative;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeIsNegative(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.IsNegative;

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
