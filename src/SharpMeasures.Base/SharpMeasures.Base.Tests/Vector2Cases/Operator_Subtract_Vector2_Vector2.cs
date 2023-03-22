namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Operator_Subtract_Vector2_Vector2
{
    private static Vector2 Target(Vector2 a, Vector2 b) => a - b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void MatchMethod(Vector2 a, Vector2 b)
    {
        var expected = Vector2.Subtract(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
