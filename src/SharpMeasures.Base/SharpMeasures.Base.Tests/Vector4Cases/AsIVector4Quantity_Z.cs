namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class AsIVector4Quantity_Z
{
    private static Scalar Target(Vector4 vector)
    {
        return execute(vector);

        static Scalar execute(IVector4Quantity vector) => vector.Z;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchZ(Vector4 vector)
    {
        var expected = vector.Z;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
