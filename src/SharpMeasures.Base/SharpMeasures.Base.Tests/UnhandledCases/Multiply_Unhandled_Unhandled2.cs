namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Multiply_Unhandled_Unhandled2
{
    private static Unhandled2 Target(Unhandled a, Unhandled2 b) => Unhandled.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled2))]
    public void MatchInstanceMethod(Unhandled a, Unhandled2 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
