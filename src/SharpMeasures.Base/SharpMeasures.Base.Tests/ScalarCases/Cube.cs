namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Cube
{
    private static Scalar Target(Scalar scalar) => scalar.Cube();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchArithmetic(Scalar scalar)
    {
        var expected = scalar * scalar * scalar;

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
