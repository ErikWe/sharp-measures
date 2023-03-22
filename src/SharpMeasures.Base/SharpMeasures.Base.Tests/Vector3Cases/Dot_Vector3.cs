namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Dot_Vector3
{
    private static Scalar Target(Vector3 vector, Vector3 factor) => vector.Dot(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchArithmetic(Vector3 vector, Vector3 factor)
    {
        var expected = (vector.X * factor.X) + (vector.Y * factor.Y) + (vector.Z * factor.Z);

        var actual = Target(vector, factor);

        Assert.Equal(expected, actual);
    }
}
