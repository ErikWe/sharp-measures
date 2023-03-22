namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Operator_Multiply_Unhandled4_Scalar
{
    private static Unhandled4 Target(Unhandled4 a, Scalar b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidScalar))]
    public void MatchMethod(Unhandled4 a, Scalar b)
    {
        var expected = Unhandled4.Multiply(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
