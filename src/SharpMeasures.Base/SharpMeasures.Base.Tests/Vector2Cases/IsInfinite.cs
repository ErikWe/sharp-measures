namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class IsInfinite
{
    private static bool Target(Vector2 vector) => vector.IsInfinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchAnyComponentIsInfinite(Vector2 vector)
    {
        var expected = vector.X.IsInfinite || vector.Y.IsInfinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
