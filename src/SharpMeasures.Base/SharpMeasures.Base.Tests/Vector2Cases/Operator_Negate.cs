namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Operator_Negate
{
    private static Vector2 Target(Vector2 vector) => -vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchMethod(Vector2 vector)
    {
        var expected = vector.Negate();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
