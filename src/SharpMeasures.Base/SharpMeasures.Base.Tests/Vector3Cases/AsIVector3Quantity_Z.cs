namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class AsIVector3Quantity_Z
{
    private static Scalar Target(Vector3 vector)
    {
        return execute(vector);

        static Scalar execute(IVector3Quantity vector) => vector.Z;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchZ(Vector3 vector)
    {
        var expected = vector.Z;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
