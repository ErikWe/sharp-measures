namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Equals_Vector2
{
    private static bool Target(Vector2 vector, Vector2 other) => vector.Equals(other);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void Valid_MatchComponentsEquals(Vector2 vector, Vector2 other)
    {
        var expected = vector.X.Equals(other.X) && vector.Y.Equals(other.Y);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
