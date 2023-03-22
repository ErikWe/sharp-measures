namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Scalar_Vector3
{
    private static Vector3 Target(Scalar a, Vector3 b) => Scalar.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector3))]
    public void MatchInstanceMethod(Scalar a, Vector3 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
