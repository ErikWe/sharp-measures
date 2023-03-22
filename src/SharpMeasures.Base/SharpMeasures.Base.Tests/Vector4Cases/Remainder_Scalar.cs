namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Remainder_Scalar
{
    private static Vector4 Target(Vector4 vector, Scalar divisor) => vector.Remainder(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchXRemainder(Vector4 vector, Scalar divisor)
    {
        var expected = vector.X.Remainder(divisor);

        var actual = Target(vector, divisor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchYRemainder(Vector4 vector, Scalar divisor)
    {
        var expected = vector.Y.Remainder(divisor);

        var actual = Target(vector, divisor).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchZRemainder(Vector4 vector, Scalar divisor)
    {
        var expected = vector.Z.Remainder(divisor);

        var actual = Target(vector, divisor).Z;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchWRemainder(Vector4 vector, Scalar divisor)
    {
        var expected = vector.W.Remainder(divisor);

        var actual = Target(vector, divisor).W;

        Assert.Equal(expected, actual);
    }
}
