namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Negate
{
    private static Unhandled Target(Unhandled unhandled) => unhandled.Negate();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeNegate(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.Negate();

        var actual = Target(unhandled).Magnitude;

        Assert.Equal(expected, actual);
    }
}
