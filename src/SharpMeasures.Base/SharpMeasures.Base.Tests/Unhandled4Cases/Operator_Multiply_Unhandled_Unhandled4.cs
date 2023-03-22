namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Operator_Multiply_Unhandled_Unhandled4
{
    private static Unhandled4 Target(Unhandled a, Unhandled4 b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled))]
    public void MatchMethod(Unhandled4 b, Unhandled a)
    {
        var expected = Unhandled4.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
