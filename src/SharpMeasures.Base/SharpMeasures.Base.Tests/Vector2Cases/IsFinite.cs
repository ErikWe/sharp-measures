namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class IsFinite
{
    private static bool Target(Vector2 vector) => vector.IsFinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchBothComponentsAreFinite(Vector2 vector)
    {
        var expected = vector.X.IsFinite && vector.Y.IsFinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
