namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Deconstruct
{
    private static (Scalar X, Scalar Y) Target(Vector2 vector)
    {
        vector.Deconstruct(out var x, out var y);

        return (x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchX(Vector2 vector)
    {
        var expected = vector.X;

        var actual = Target(vector).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchY(Vector2 vector)
    {
        var expected = vector.Y;

        var actual = Target(vector).Y;

        Assert.Equal(expected, actual);
    }
}
