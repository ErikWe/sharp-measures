namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Operator_Inequality
{
    private static bool Target(Vector2 lhs, Vector2 rhs) => lhs != rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void Valid_OppositeOfEqualsMethod(Vector2 lhs, Vector2 rhs)
    {
        var expected = lhs.Equals(rhs) is false;

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
