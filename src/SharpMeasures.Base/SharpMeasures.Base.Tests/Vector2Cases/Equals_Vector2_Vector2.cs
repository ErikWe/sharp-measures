namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Equals_Vector2_Vector2
{
    private static bool Target(Vector2 lhs, Vector2 rhs) => Vector2.Equals(lhs, rhs);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void Valid_MatchInstanceMethod(Vector2 lhs, Vector2 rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
