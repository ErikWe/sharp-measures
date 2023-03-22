namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Deconstruct
{
    private static (Scalar X, Scalar Y, Scalar Z, Scalar W) Target(Vector4 vector)
    {
        vector.Deconstruct(out var x, out var y, out var z, out var w);

        return (x, y, z, w);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchX(Vector4 vector)
    {
        var expected = vector.X;

        var actual = Target(vector).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchY(Vector4 vector)
    {
        var expected = vector.Y;

        var actual = Target(vector).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchZ(Vector4 vector)
    {
        var expected = vector.Z;

        var actual = Target(vector).Z;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchW(Vector4 vector)
    {
        var expected = vector.W;

        var actual = Target(vector).W;

        Assert.Equal(expected, actual);
    }
}
