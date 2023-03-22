namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class ToValueTuple
{
    private static (Scalar X, Scalar Y, Scalar Z) Target(Vector3 vector) => vector.ToValueTuple();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchX(Vector3 vector)
    {
        var expected = vector.X;

        var actual = Target(vector).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchY(Vector3 vector)
    {
        var expected = vector.Y;

        var actual = Target(vector).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchZ(Vector3 vector)
    {
        var expected = vector.Z;

        var actual = Target(vector).Z;

        Assert.Equal(expected, actual);
    }
}
