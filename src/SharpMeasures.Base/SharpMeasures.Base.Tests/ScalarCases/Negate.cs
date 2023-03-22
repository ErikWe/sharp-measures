namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Negate
{
    private static Scalar Target(Scalar scalar) => scalar.Negate();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchToDoubleNegation(Scalar scalar)
    {
        var expected = -scalar.ToDouble();

        var actual = Target(scalar).ToDouble();

        Assert.Equal(expected, actual);
    }
}
