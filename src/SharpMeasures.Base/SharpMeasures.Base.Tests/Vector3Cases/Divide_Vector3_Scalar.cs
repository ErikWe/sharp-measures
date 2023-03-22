namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Divide_Vector3_Scalar
{
    private static Vector3 Target(Vector3 a, Scalar b) => Vector3.Divide(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchInstanceMethod(Vector3 a, Scalar b)
    {
        var expected = a.DivideBy(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
