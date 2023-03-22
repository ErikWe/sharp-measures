namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Multiply_Unhandled
{
    private static Unhandled2 Target(Vector2 vector, Unhandled factor) => vector.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidUnhandled))]
    public void MatchXMultiply(Vector2 vector, Unhandled factor)
    {
        var expected = vector.X.Multiply(factor);

        var actual = Target(vector, factor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidUnhandled))]
    public void MatchYMultiply(Vector2 vector, Unhandled factor)
    {
        var expected = vector.Y.Multiply(factor);

        var actual = Target(vector, factor).Y;

        Assert.Equal(expected, actual);
    }
}
