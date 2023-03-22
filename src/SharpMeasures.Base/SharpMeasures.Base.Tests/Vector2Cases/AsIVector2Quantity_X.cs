namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class AsIVector2Quantity_X
{
    private static Scalar Target(Vector2 vector)
    {
        return execute(vector);

        static Scalar execute(IVector2Quantity vector) => vector.X;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchX(Vector2 vector)
    {
        var expected = vector.X;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
