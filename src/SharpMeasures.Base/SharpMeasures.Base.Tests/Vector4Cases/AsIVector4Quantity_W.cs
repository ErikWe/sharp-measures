namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class AsIVector4Quantity_W
{
    private static Scalar Target(Vector4 vector)
    {
        return execute(vector);

        static Scalar execute(IVector4Quantity vector) => vector.W;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchW(Vector4 vector)
    {
        var expected = vector.W;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
