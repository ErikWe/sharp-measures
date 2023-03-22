namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_Multiply_Vector2_Scalar
{
    private static Vector2 Target((Scalar, Scalar) a, Scalar b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector2))]
    public void MatchMethod(Scalar b, Vector2 a)
    {
        var expected = Vector2.Multiply(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
