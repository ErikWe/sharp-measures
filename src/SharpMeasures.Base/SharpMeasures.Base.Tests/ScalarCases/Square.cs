namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Square
{
    private static Scalar Target(Scalar scalar) => scalar.Square();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchArithmetic(Scalar scalar)
    {
        var expected = scalar * scalar;

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
