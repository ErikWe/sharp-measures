namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Scalar_Unhandled2
{
    private static Unhandled2 Target(Scalar a, Unhandled2 b) => Scalar.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidUnhandled2))]
    public void MatchInstanceMethod(Scalar a, Unhandled2 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
