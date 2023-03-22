namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Constructor
{
    private static Vector4 Target(Scalar x, Scalar y, Scalar z, Scalar w) => new(x, y, z, w);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchX(Scalar x, Scalar y, Scalar z, Scalar w)
    {
        var actual = Target(x, y, z, w).X;

        Assert.Equal(x, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchY(Scalar x, Scalar y, Scalar z, Scalar w)
    {
        var actual = Target(x, y, z, w).Y;

        Assert.Equal(y, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchZ(Scalar x, Scalar y, Scalar z, Scalar w)
    {
        var actual = Target(x, y, z, w).Z;

        Assert.Equal(z, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchW(Scalar x, Scalar y, Scalar z, Scalar w)
    {
        var actual = Target(x, y, z, w).W;

        Assert.Equal(w, actual);
    }
}
