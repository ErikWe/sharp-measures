namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Dot_Vector3_Vector3
{
    private static Scalar Target(Vector3 a, Vector3 b) => Vector3.Dot(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchInstanceMethod(Vector3 a, Vector3 b)
    {
        var expected = a.Dot(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
