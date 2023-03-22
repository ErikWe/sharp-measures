namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Multiply_Vector2_Unhandled
{
    private static Unhandled2 Target(Vector2 a, Unhandled b) => Vector2.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidUnhandled))]
    public void MatchInstanceMethod(Vector2 a, Unhandled b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
