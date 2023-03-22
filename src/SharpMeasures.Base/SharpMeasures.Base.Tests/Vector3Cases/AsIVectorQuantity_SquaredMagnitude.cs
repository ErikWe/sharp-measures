namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class AsIVectorQuantity_SquaredMagnitude
{
    private static Scalar Target(Vector3 vector)
    {
        return execute(vector);

        static Scalar execute(IVectorQuantity vector) => vector.SquaredMagnitude();
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchSquaredMagnitude(Vector3 vector)
    {
        var expected = vector.SquaredMagnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
