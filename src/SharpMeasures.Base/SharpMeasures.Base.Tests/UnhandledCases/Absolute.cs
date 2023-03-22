namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Absolute
{
    private static Unhandled Target(Unhandled unhandled) => unhandled.Absolute();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeAbsolute(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.Absolute();

        var actual = Target(unhandled).Magnitude;

        Assert.Equal(expected, actual);
    }
}
