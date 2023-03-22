namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class AsIVectorQuantity_Magnitude
{
    private static Scalar Target(Vector3 vector)
    {
        return execute(vector);

        static Scalar execute(IVectorQuantity vector) => vector.Magnitude();
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchMagnitude(Vector3 vector)
    {
        var expected = vector.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
