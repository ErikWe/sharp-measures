namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Multiply_Unhandled_Vector2
{
    private static Unhandled2 Target(Unhandled x, Vector2 y) => x * y;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector2))]
    public void MatchMethod(Unhandled x, Vector2 y)
    {
        var expected = Unhandled.Multiply(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
