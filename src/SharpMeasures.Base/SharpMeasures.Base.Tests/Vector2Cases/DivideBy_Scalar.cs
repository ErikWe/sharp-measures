namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class DivideBy_Scalar
{
    private static Vector2 Target(Vector2 vector, Scalar divisor) => vector.DivideBy(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidScalar))]
    public void MatchXDivideBy(Vector2 vector, Scalar divisor)
    {
        var expected = vector.X.DivideBy(divisor);

        var actual = Target(vector, divisor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidScalar))]
    public void MatchYDivideBy(Vector2 vector, Scalar divisor)
    {
        var expected = vector.Y.DivideBy(divisor);

        var actual = Target(vector, divisor).Y;

        Assert.Equal(expected, actual);
    }
}
