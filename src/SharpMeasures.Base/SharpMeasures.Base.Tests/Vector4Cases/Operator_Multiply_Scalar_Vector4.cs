namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Operator_Multiply_Scalar_Vector4
{
    private static Vector4 Target(Scalar a, Vector4 b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchInstanceMethod(Vector4 b, Scalar a)
    {
        var expected = Vector4.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
