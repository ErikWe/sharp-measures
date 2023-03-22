namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class ToScalar
{
    private static Scalar Target(Unhandled unhandled) => unhandled.ToScalar();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void Valid_MatchMagnitude(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude;

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
