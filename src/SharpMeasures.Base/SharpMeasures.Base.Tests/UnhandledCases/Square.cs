namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Square
{
    private static Unhandled Target(Unhandled unhandled) => unhandled.Square();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitudeSquare(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.Square();

        var actual = Target(unhandled).Magnitude;

        Assert.Equal(expected, actual);
    }
}
