namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class IsZero
{
    private static bool Target(Vector2 vector) => vector.IsZero;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchBothComponentsAreZero(Vector2 vector)
    {
        var expected = vector.X.IsZero && vector.Y.IsZero;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
