namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Unhandled2
{
    private static Unhandled2 Target(Scalar scalar, Unhandled2 factor) => scalar.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidUnhandled2))]
    public void MatchMultiply2(Scalar scalar, Unhandled2 factor)
    {
        var expected = scalar.Multiply2(factor);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }
}
