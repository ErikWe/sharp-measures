namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Scalar
{
    private static Scalar Target(Scalar scalar, Scalar factor) => scalar.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchToDoubleMultiplication(Scalar scalar, Scalar factor)
    {
        var expected = scalar.ToDouble() * factor.ToDouble();

        var actual = Target(scalar, factor).ToDouble();

        Assert.Equal(expected, actual);
    }
}
