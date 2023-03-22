namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Scalar_Vector2
{
    private static Vector2 Target(Scalar a, Vector2 b) => Scalar.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector2))]
    public void MatchInstanceMethod(Scalar a, Vector2 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
