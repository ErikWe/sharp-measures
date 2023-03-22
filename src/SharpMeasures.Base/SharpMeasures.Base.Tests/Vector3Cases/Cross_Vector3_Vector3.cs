namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Cross_Vector3_Vector3
{
    private static Vector3 Target(Vector3 a, Vector3 b) => Vector3.Cross(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchInstanceMethod(Vector3 a, Vector3 b)
    {
        var expected = a.Cross(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
