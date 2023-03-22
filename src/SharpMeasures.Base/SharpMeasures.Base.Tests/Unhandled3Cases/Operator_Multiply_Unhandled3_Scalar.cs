namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Operator_Multiply_Unhandled3_Scalar
{
    private static Unhandled3 Target(Unhandled3 a, Scalar b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidScalar))]
    public void MatchMethod(Unhandled3 a, Scalar b)
    {
        var expected = Unhandled3.Multiply(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
