namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Cross_Vector3
{
    private static Vector3 Target(Vector3 vector, Vector3 factor) => vector.Cross(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchArithmetic(Vector3 vector, Vector3 factor)
    {
        Vector3 expected =
        (
            (vector.Y * factor.Z) - (vector.Z * factor.Y),
            (vector.Z * factor.X) - (vector.X * factor.Z),
            (vector.X * factor.Y) - (vector.Y * factor.X)
        );

        var actual = Target(vector, factor);

        Assert.Equal(expected, actual);
    }
}
