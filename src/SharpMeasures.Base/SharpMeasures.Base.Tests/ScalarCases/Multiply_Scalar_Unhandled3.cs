namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Scalar_Unhandled3
{
    private static Unhandled3 Target(Scalar a, Unhandled3 b) => Scalar.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidUnhandled3))]
    public void MatchInstanceMethod(Scalar a, Unhandled3 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
