namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Remainder_Vector2_Scalar
{
    private static Vector2 Target(Vector2 a, Scalar b) => Vector2.Remainder(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidScalar))]
    public void MatchInstanceMethod(Vector2 a, Scalar b)
    {
        var expected = a.Remainder(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
