namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Equals_Unhandled
{
    private static bool Target(Unhandled unhandled, Unhandled other) => unhandled.Equals(other);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled))]
    public void Valid_MatchMagnitudeEquals(Unhandled unhandled, Unhandled other)
    {
        var expected = unhandled.Magnitude.Equals(other.Magnitude);

        var actual = Target(unhandled, other);

        Assert.Equal(expected, actual);
    }
}
