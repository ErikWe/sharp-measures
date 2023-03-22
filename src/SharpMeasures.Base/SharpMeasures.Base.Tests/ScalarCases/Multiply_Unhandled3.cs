namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Unhandled3
{
    private static Unhandled3 Target(Scalar scalar, Unhandled3 factor) => scalar.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidUnhandled3))]
    public void MatchMultiply3(Scalar scalar, Unhandled3 factor)
    {
        var expected = scalar.Multiply3(factor);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }
}
