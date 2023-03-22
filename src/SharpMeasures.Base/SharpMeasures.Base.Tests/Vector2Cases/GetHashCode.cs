namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class GetHashCode
{
    private static int Target(Vector2 vector) => vector.GetHashCode();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void Equal_Match(Vector2 vector)
    {
        Vector2 other = new(vector.X, vector.Y);

        var expected = Target(other);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
