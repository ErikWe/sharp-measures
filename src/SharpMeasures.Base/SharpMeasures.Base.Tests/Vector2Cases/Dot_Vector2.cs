namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Dot_Vector2
{
    private static Scalar Target(Vector2 vector, Vector2 factor) => vector.Dot(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void MatchArithmetic(Vector2 vector, Vector2 factor)
    {
        var expected = (vector.X * factor.X) + (vector.Y * factor.Y);

        var actual = Target(vector, factor);

        Assert.Equal(expected, actual);
    }
}
