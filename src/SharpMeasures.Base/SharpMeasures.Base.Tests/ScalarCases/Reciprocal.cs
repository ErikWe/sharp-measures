namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Reciprocal
{
    private static Scalar Target(Scalar scalar) => scalar.Reciprocal();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchArithmetic(Scalar scalar)
    {
        var expected = 1 / scalar;

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
