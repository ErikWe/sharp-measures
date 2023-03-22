namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class SquaredMagnitude
{
    private static Scalar Target(Vector2 vector) => vector.SquaredMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchArithmetic(Vector2 vector)
    {
        var expected = (vector.X * vector.X) + (vector.Y * vector.Y);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
