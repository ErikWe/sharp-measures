namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Scalar_Unhandled4
{
    private static Unhandled4 Target(Scalar a, Unhandled4 b) => Scalar.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidUnhandled4))]
    public void MatchInstanceMethod(Scalar a, Unhandled4 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
