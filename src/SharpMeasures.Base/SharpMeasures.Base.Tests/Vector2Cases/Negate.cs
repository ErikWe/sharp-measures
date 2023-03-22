namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Negate
{
    private static Vector2 Target(Vector2 vector) => vector.Negate();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchNegatedX(Vector2 vector)
    {
        var expected = vector.X.Negate();

        var actual = Target(vector).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchNegatedY(Vector2 vector)
    {
        var expected = vector.Y.Negate();

        var actual = Target(vector).Y;

        Assert.Equal(expected, actual);
    }
}
