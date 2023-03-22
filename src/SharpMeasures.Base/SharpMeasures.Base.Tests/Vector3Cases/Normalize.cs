namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Normalize
{
    private static Vector3 Target(Vector3 vector) => vector.Normalize();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchArithmetic(Vector3 vector)
    {
        var expected = vector / vector.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
