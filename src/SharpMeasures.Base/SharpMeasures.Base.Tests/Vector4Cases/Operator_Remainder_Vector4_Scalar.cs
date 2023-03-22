namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Operator_Remainder_Vector4_Scalar
{
    private static Vector4 Target(Vector4 a, Scalar b) => a % b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchMethod(Vector4 a, Scalar b)
    {
        var expected = Vector4.Remainder(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
