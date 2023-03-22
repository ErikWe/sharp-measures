namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class SquareRoot
{
    private static Unhandled Target(Unhandled unhandled) => unhandled.SquareRoot();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeSquareRoot(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.SquareRoot();

        var actual = Target(unhandled).Magnitude;

        Assert.Equal(expected, actual);
    }
}
