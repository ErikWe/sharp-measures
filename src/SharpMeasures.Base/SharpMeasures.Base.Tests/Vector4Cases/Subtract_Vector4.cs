namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Subtract_Vector4
{
    private static Vector4 Target(Vector4 vector, Vector4 subtrahend) => vector.Subtract(subtrahend);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchXSubtract(Vector4 vector, Vector4 subtrahend)
    {
        var expected = vector.X.Subtract(subtrahend.X);

        var actual = Target(vector, subtrahend).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchYSubtract(Vector4 vector, Vector4 subtrahend)
    {
        var expected = vector.Y.Subtract(subtrahend.Y);

        var actual = Target(vector, subtrahend).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchZSubtract(Vector4 vector, Vector4 subtrahend)
    {
        var expected = vector.Z.Subtract(subtrahend.Z);

        var actual = Target(vector, subtrahend).Z;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchWSubtract(Vector4 vector, Vector4 subtrahend)
    {
        var expected = vector.W.Subtract(subtrahend.W);

        var actual = Target(vector, subtrahend).W;

        Assert.Equal(expected, actual);
    }
}
