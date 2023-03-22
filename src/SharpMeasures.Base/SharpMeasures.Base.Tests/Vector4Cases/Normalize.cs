namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Normalize
{
    private static Vector4 Target(Vector4 vector) => vector.Normalize();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchArithmetic(Vector4 vector)
    {
        var expected = vector / vector.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
