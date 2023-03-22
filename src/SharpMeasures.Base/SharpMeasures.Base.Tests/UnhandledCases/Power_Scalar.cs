namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Power_Scalar
{
    private static Unhandled Target(Unhandled unhandled, Scalar exponent) => unhandled.Power(exponent);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMagnitudePower(Unhandled unhandled, Scalar exponent)
    {
        var expected = unhandled.Magnitude.Power(exponent);

        var actual = Target(unhandled, exponent).Magnitude;

        Assert.Equal(expected, actual);
    }
}
