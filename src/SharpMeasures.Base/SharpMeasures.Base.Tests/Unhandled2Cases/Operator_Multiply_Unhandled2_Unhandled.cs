namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Operator_Multiply_Unhandled2_Unhandled
{
    private static Unhandled2 Target(Unhandled2 a, Unhandled b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled))]
    public void MatchMethod(Unhandled2 a, Unhandled b)
    {
        var expected = Unhandled2.Multiply(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
