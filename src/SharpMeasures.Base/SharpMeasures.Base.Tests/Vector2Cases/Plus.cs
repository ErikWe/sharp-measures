namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Plus
{
    private static Vector2 Target(Vector2 vector) => vector.Plus();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void EqualOriginal(Vector2 vector)
    {
        var actual = Target(vector);

        Assert.Equal(vector, actual);
    }
}
