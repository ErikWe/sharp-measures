namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class AsIVector3Quantity_X
{
    private static Scalar Target(Vector3 vector)
    {
        return execute(vector);

        static Scalar execute(IVector3Quantity vector) => vector.X;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchX(Vector3 vector)
    {
        var expected = vector.X;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
