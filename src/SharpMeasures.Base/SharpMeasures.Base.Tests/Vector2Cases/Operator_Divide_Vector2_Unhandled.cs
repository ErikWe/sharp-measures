namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Operator_Divide_Vector2_Unhandled
{
    private static Unhandled2 Target(Vector2 a, Unhandled b) => a / b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidUnhandled))]
    public void MatchInstanceMethod(Vector2 a, Unhandled b)
    {
        var expected = Vector2.Divide(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
