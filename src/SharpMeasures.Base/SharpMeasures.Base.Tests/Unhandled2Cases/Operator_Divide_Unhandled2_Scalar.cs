namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Operator_Divide_Unhandled2_Scalar
{
    private static Unhandled2 Target(Unhandled2 a, Scalar b) => a / b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidScalar))]
    public void MatchMethod(Unhandled2 a, Scalar b)
    {
        var expected = Unhandled2.Divide(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
