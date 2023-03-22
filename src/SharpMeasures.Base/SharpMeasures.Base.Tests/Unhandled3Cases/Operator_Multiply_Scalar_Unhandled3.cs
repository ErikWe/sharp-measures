namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Operator_Multiply_Scalar_Unhandled3
{
    private static Unhandled3 Target(Scalar a, Unhandled3 b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidScalar))]
    public void MatchMethod(Unhandled3 b, Scalar a)
    {
        var expected = Unhandled3.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
