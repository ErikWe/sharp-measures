namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Dot_Vector4
{
    private static Scalar Target(Vector4 vector, Vector4 factor) => vector.Dot(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchArithmetic(Vector4 vector, Vector4 factor)
    {
        var expected = (vector.X * factor.X) + (vector.Y * factor.Y) + (vector.Z * factor.Z) + (vector.W * factor.W);

        var actual = Target(vector, factor);

        Assert.Equal(expected, actual);
    }
}
