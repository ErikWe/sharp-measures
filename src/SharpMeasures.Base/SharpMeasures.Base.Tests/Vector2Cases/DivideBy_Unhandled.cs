namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class DivideBy_Unhandled
{
    private static Unhandled2 Target(Vector2 vector, Unhandled divisor) => vector.DivideBy(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidUnhandled))]
    public void MatchXDivideBy(Vector2 vector, Unhandled divisor)
    {
        var expected = vector.X.DivideBy(divisor);

        var actual = Target(vector, divisor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidUnhandled))]
    public void MatchYDivideBy(Vector2 vector, Unhandled divisor)
    {
        var expected = vector.Y.DivideBy(divisor);

        var actual = Target(vector, divisor).Y;

        Assert.Equal(expected, actual);
    }
}
