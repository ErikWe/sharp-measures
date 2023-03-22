namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class DivideBy_Scalar
{
    private static Scalar Target(Scalar scalar, Scalar divisor) => scalar.DivideBy(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchToDoubleDivision(Scalar scalar, Scalar divisor)
    {
        var expected = scalar.ToDouble() / divisor.ToDouble();

        var actual = Target(scalar, divisor).ToDouble();

        Assert.Equal(expected, actual);
    }
}
