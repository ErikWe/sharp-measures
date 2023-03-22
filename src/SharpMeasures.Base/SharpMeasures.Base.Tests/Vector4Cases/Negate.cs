namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Negate
{
    private static Vector4 Target(Vector4 vector) => vector.Negate();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchNegatedX(Vector4 vector)
    {
        var expected = vector.X.Negate();

        var actual = Target(vector).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchNegatedY(Vector4 vector)
    {
        var expected = vector.Y.Negate();

        var actual = Target(vector).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchNegatedZ(Vector4 vector)
    {
        var expected = vector.Z.Negate();

        var actual = Target(vector).Z;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchNegatedW(Vector4 vector)
    {
        var expected = vector.W.Negate();

        var actual = Target(vector).W;

        Assert.Equal(expected, actual);
    }
}
