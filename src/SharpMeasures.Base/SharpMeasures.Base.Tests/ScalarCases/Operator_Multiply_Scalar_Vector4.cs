namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_Multiply_Scalar_Vector4
{
    private static Vector4 Target(Scalar a, (Scalar, Scalar, Scalar, Scalar) b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector4))]
    public void MatchMethod(Scalar a, Vector4 b)
    {
        var expected = Scalar.Multiply(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
