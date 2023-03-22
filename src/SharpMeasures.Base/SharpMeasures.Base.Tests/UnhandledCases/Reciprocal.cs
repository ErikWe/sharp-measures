namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Reciprocal
{
    private static Unhandled Target(Unhandled unhandled) => unhandled.Reciprocal();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeReciprocal(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.Reciprocal();

        var actual = Target(unhandled).Magnitude;

        Assert.Equal(expected, actual);
    }
}
