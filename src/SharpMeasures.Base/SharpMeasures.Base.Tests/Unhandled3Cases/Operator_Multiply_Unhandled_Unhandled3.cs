namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Operator_Multiply_Unhandled_Unhandled3
{
    private static Unhandled3 Target(Unhandled a, Unhandled3 b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled))]
    public void MatchMethod(Unhandled3 b, Unhandled a)
    {
        var expected = Unhandled3.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
