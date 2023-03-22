namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Operator_Multiply_Unhandled_Unhandled2
{
    private static Unhandled2 Target(Unhandled a, Unhandled2 b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled))]
    public void MatchMethod(Unhandled2 b, Unhandled a)
    {
        var expected = Unhandled2.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
