namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class AsIVectorQuantity_SquaredMagnitude
{
    private static Scalar Target(Vector2 vector)
    {
        return execute(vector);

        static Scalar execute(IVectorQuantity vector) => vector.SquaredMagnitude();
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchSquaredMagnitude(Vector2 vector)
    {
        var expected = vector.SquaredMagnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
