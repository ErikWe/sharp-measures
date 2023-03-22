namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class AsIVectorQuantity_Magnitude
{
    private static Scalar Target(Vector2 vector)
    {
        return execute(vector);

        static Scalar execute(IVectorQuantity vector) => vector.Magnitude();
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchMagnitude(Vector2 vector)
    {
        var expected = vector.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
