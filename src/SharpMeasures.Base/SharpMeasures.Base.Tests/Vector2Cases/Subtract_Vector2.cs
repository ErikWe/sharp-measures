namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Subtract_Vector2
{
    private static Vector2 Target(Vector2 vector, Vector2 subtrahend) => vector.Subtract(subtrahend);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void MatchXSubtract(Vector2 vector, Vector2 subtrahend)
    {
        var expected = vector.X.Subtract(subtrahend.X);

        var actual = Target(vector, subtrahend).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void MatchYSubtract(Vector2 vector, Vector2 subtrahend)
    {
        var expected = vector.Y.Subtract(subtrahend.Y);

        var actual = Target(vector, subtrahend).Y;

        Assert.Equal(expected, actual);
    }
}
