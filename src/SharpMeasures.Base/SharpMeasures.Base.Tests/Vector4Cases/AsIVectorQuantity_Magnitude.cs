namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class AsIVectorQuantity_Magnitude
{
    private static Scalar Target(Vector4 vector)
    {
        return execute(vector);

        static Scalar execute(IVectorQuantity vector) => vector.Magnitude();
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchMagnitude(Vector4 vector)
    {
        var expected = vector.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
