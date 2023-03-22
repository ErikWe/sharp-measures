namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Multiply_Scalar
{
    private static Vector2 Target(Vector2 vector, Scalar factor) => vector.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidScalar))]
    public void MatchXMultiply(Vector2 vector, Scalar factor)
    {
        var expected = vector.X.Multiply(factor);

        var actual = Target(vector, factor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidScalar))]
    public void MatchYMultiply(Vector2 vector, Scalar factor)
    {
        var expected = vector.Y.Multiply(factor);

        var actual = Target(vector, factor).Y;

        Assert.Equal(expected, actual);
    }
}
