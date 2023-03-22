namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class AsIVector3Quantity_Y
{
    private static Scalar Target(Vector3 vector)
    {
        return execute(vector);

        static Scalar execute(IVector3Quantity vector) => vector.Y;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchY(Vector3 vector)
    {
        var expected = vector.Y;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
