namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Multiply_Unhandled
{
    private static Unhandled4 Target(Vector4 vector, Unhandled factor) => vector.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidUnhandled))]
    public void MatchXMultiply(Vector4 vector, Unhandled factor)
    {
        var expected = vector.X.Multiply(factor);

        var actual = Target(vector, factor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidUnhandled))]
    public void MatchYMultiply(Vector4 vector, Unhandled factor)
    {
        var expected = vector.Y.Multiply(factor);

        var actual = Target(vector, factor).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidUnhandled))]
    public void MatchZMultiply(Vector4 vector, Unhandled factor)
    {
        var expected = vector.Z.Multiply(factor);

        var actual = Target(vector, factor).Z;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidUnhandled))]
    public void MatchWMultiply(Vector4 vector, Unhandled factor)
    {
        var expected = vector.W.Multiply(factor);

        var actual = Target(vector, factor).W;

        Assert.Equal(expected, actual);
    }
}
