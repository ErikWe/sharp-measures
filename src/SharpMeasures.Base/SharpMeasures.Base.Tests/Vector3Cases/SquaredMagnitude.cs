namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class SquaredMagnitude
{
    private static Scalar Target(Vector3 vector) => vector.SquaredMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchArithmetic(Vector3 vector)
    {
        var expected = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
