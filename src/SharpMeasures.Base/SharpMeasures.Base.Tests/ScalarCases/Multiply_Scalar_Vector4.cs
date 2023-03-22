namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Scalar_Vector4
{
    private static Vector4 Target(Scalar a, Vector4 b) => Scalar.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector4))]
    public void MatchInstanceMethod(Scalar a, Vector4 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
