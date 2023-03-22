namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Dot_Vector2_Vector2
{
    private static Scalar Target(Vector2 a, Vector2 b) => Vector2.Dot(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void MatchInstanceMethod(Vector2 a, Vector2 b)
    {
        var expected = a.Dot(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
