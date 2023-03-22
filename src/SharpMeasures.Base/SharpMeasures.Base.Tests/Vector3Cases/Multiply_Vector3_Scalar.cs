namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Multiply_Vector3_Scalar
{
    private static Vector3 Target(Vector3 a, Scalar b) => Vector3.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchInstanceMethod(Vector3 a, Scalar b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
