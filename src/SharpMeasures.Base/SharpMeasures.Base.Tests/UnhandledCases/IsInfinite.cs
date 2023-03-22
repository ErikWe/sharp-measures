namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class IsInfinite
{
    private static bool Target(Unhandled unhandled) => unhandled.IsInfinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeIsInfinity(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.IsInfinite;

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
