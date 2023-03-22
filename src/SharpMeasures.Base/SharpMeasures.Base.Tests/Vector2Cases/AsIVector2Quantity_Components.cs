namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class AsIVector2Quantity_Components
{
    private static Vector2 Target(Vector2 vector)
    {
        return execute(vector);

        static Vector2 execute(IVector2Quantity vector) => vector.Components;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchOriginal(Vector2 vector)
    {
        var actual = Target(vector);

        Assert.Equal(vector, actual);
    }
}
