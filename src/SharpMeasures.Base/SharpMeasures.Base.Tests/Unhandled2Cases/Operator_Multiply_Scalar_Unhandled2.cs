namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Operator_Multiply_Scalar_Unhandled2
{
    private static Unhandled2 Target(Scalar a, Unhandled2 b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidScalar))]
    public void MatchMethod(Unhandled2 b, Scalar a)
    {
        var expected = Unhandled2.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
