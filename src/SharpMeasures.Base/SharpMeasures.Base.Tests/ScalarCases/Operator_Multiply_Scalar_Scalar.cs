namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_Multiply_Scalar_Scalar
{
    private static Scalar Target(Scalar x, Scalar y) => x * y;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchMethod(Scalar x, Scalar y)
    {
        var expected = Scalar.Multiply(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
