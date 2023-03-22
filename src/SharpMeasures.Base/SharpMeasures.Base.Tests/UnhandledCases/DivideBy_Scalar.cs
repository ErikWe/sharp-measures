namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class DivideBy_Scalar
{
    private static Unhandled Target(Unhandled unhandled, Scalar divisor) => unhandled.DivideBy(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMagnitudeDivideBy(Unhandled unhandled, Scalar divisor)
    {
        var expected = unhandled.Magnitude.DivideBy(divisor);

        var actual = Target(unhandled, divisor).Magnitude;

        Assert.Equal(expected, actual);
    }
}
