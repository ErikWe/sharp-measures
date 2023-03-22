namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_Multiply_Vector4_Scalar
{
    private static Vector4 Target((Scalar, Scalar, Scalar, Scalar) a, Scalar b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector4))]
    public void MatchMethod(Scalar b, Vector4 a)
    {
        var expected = Vector4.Multiply(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
