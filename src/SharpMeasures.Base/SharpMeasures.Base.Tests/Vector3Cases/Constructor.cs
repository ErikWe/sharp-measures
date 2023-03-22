namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Constructor
{
    private static Vector3 Target(Scalar x, Scalar y, Scalar z) => new(x, y, z);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchX(Scalar x, Scalar y, Scalar z)
    {
        var actual = Target(x, y, z).X;

        Assert.Equal(x, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchY(Scalar x, Scalar y, Scalar z)
    {
        var actual = Target(x, y, z).Y;

        Assert.Equal(y, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchZ(Scalar x, Scalar y, Scalar z)
    {
        var actual = Target(x, y, z).Z;

        Assert.Equal(z, actual);
    }
}
