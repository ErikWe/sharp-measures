namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class AsIVector4Quantity_Y
{
    private static Scalar Target(Vector4 vector)
    {
        return execute(vector);

        static Scalar execute(IVector4Quantity vector) => vector.Y;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchY(Vector4 vector)
    {
        var expected = vector.Y;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
