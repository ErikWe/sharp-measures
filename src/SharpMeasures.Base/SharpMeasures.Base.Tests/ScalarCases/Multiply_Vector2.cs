namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Vector2
{
    private static Vector2 Target(Scalar scalar, Vector2 factor) => scalar.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector2))]
    public void MatchMultiply2(Scalar scalar, Vector2 factor)
    {
        var expected = scalar.Multiply2(factor);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }
}
