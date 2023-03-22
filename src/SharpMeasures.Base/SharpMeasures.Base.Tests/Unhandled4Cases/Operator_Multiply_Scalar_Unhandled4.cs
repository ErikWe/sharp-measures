namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Operator_Multiply_Scalar_Unhandled4
{
    private static Unhandled4 Target(Scalar a, Unhandled4 b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidScalar))]
    public void MatchMethod(Unhandled4 b, Scalar a)
    {
        var expected = Unhandled4.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
