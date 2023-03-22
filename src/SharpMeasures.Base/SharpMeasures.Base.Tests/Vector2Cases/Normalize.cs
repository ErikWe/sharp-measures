namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Normalize
{
    private static Vector2 Target(Vector2 vector) => vector.Normalize();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchArithmetic(Vector2 vector)
    {
        var expected = vector / vector.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
