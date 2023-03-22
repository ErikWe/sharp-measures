namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Multiply_Scalar
{
    private static Vector4 Target(Vector4 vector, Scalar factor) => vector.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchXMultiply(Vector4 vector, Scalar factor)
    {
        var expected = vector.X.Multiply(factor);

        var actual = Target(vector, factor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchYMultiply(Vector4 vector, Scalar factor)
    {
        var expected = vector.Y.Multiply(factor);

        var actual = Target(vector, factor).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchZMultiply(Vector4 vector, Scalar factor)
    {
        var expected = vector.Z.Multiply(factor);

        var actual = Target(vector, factor).Z;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchWMultiply(Vector4 vector, Scalar factor)
    {
        var expected = vector.W.Multiply(factor);

        var actual = Target(vector, factor).W;

        Assert.Equal(expected, actual);
    }
}
