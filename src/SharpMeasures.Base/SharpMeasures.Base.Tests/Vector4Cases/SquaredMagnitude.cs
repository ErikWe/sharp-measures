namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class SquaredMagnitude
{
    private static Scalar Target(Vector4 vector) => vector.SquaredMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchArithmetic(Vector4 vector)
    {
        var expected = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
