namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Operator_Equality
{
    private static bool Target(Vector2 lhs, Vector2 rhs) => lhs == rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void Valid_MatchEqualsMethod(Vector2 lhs, Vector2 rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
