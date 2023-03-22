namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Operator_Multiply_Vector2_Unhandled
{
    private static Unhandled2 Target(Vector2 a, Unhandled b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidUnhandled))]
    public void MatchMethod(Vector2 x, Unhandled y)
    {
        var expected = Vector2.Multiply(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
