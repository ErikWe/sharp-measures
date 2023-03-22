namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Multiply_Scalar
{
    private static Unhandled Target(Unhandled unhandled, Scalar factor) => unhandled.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMagnitudeMultiply(Unhandled unhandled, Scalar factor)
    {
        var expected = unhandled.Magnitude.Multiply(factor);

        var actual = Target(unhandled, factor).Magnitude;

        Assert.Equal(expected, actual);
    }
}
