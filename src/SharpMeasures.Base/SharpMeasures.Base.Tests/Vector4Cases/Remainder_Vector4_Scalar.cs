namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Remainder_Vector4_Scalar
{
    private static Vector4 Target(Vector4 a, Scalar b) => Vector4.Remainder(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchInstanceMethod(Vector4 a, Scalar b)
    {
        var expected = a.Remainder(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
