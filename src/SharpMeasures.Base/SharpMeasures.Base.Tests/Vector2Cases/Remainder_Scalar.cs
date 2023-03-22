namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Remainder_Scalar
{
    private static Vector2 Target(Vector2 vector, Scalar divisor) => vector.Remainder(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidScalar))]
    public void MatchXRemainder(Vector2 vector, Scalar divisor)
    {
        var expected = vector.X.Remainder(divisor);

        var actual = Target(vector, divisor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidScalar))]
    public void MatchYRemainder(Vector2 vector, Scalar divisor)
    {
        var expected = vector.Y.Remainder(divisor);

        var actual = Target(vector, divisor).Y;

        Assert.Equal(expected, actual);
    }
}
