namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class AsIVector2Quantity_Y
{
    private static Scalar Target(Vector2 vector)
    {
        return execute(vector);

        static Scalar execute(IVector2Quantity vector) => vector.Y;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchY(Vector2 vector)
    {
        var expected = vector.Y;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
