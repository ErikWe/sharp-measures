namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Operator_Multiply_Scalar_Vector2
{
    private static Vector2 Target(Scalar a, Vector2 b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidScalar))]
    public void MatchInstanceMethod(Vector2 b, Scalar a)
    {
        var expected = Vector2.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
