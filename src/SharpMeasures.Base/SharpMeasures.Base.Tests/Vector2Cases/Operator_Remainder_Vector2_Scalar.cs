namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Operator_Remainder_Vector2_Scalar
{
    private static Vector2 Target(Vector2 a, Scalar b) => a % b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidScalar))]
    public void MatchMethod(Vector2 a, Scalar b)
    {
        var expected = Vector2.Remainder(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
