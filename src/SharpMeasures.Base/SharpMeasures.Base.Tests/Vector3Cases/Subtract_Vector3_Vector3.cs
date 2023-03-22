namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Subtract_Vector3_Vector3
{
    private static Vector3 Target(Vector3 a, Vector3 b) => Vector3.Subtract(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchInstanceMethod(Vector3 a, Vector3 b)
    {
        var expected = a.Subtract(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
