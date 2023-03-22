namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Vector4
{
    private static Vector4 Target(Scalar scalar, Vector4 factor) => scalar.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector4))]
    public void MatchMultiply4(Scalar scalar, Vector4 factor)
    {
        var expected = scalar.Multiply4(factor);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }
}
