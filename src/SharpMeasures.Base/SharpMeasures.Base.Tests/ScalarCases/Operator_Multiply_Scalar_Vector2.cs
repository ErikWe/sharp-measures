namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_Multiply_Scalar_Vector2
{
    private static Vector2 Target(Scalar a, (Scalar, Scalar) b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector2))]
    public void MatchMethod(Scalar a, Vector2 b)
    {
        var expected = Scalar.Multiply(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
