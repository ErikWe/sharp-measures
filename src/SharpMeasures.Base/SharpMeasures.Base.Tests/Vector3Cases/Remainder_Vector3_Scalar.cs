namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Remainder_Vector3_Scalar
{
    private static Vector3 Target(Vector3 a, Scalar b) => Vector3.Remainder(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchInstanceMethod(Vector3 a, Scalar b)
    {
        var expected = a.Remainder(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
