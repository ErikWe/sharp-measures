namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Remainder_Scalar
{
    private static Scalar Target(Scalar scalar, Scalar divisor) => scalar.Remainder(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchToDoubleRemainder(Scalar scalar, Scalar divisor)
    {
        var expected = scalar.ToDouble() % divisor.ToDouble();

        var actual = Target(scalar, divisor).ToDouble();

        Assert.Equal(expected, actual);
    }
}
