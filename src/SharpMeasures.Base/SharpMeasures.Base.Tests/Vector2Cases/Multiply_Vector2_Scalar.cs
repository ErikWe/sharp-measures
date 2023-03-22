namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Multiply_Vector2_Scalar
{
    private static Vector2 Target(Vector2 a, Scalar b) => Vector2.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidScalar))]
    public void MatchInstanceMethod(Vector2 a, Scalar b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
