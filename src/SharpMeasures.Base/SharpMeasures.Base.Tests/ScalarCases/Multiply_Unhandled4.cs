namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Unhandled4
{
    private static Unhandled4 Target(Scalar scalar, Unhandled4 factor) => scalar.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidUnhandled4))]
    public void MatchMultiply4(Scalar scalar, Unhandled4 factor)
    {
        var expected = scalar.Multiply4(factor);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }
}
