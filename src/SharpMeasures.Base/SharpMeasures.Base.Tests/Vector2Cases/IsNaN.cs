namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class IsNaN
{
    private static bool Target(Vector2 vector) => vector.IsNaN;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchAnyComponentIsNaN(Vector2 vector)
    {
        var expected = vector.X.IsNaN || vector.Y.IsNaN;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
