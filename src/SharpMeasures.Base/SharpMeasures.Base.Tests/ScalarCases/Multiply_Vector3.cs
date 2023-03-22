namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Vector3
{
    private static Vector3 Target(Scalar scalar, Vector3 factor) => scalar.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector3))]
    public void MatchMultiply2(Scalar scalar, Vector3 factor)
    {
        var expected = scalar.Multiply3(factor);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }
}
