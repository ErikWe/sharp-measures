namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Multiply_Unhandled_Unhandled3
{
    private static Unhandled3 Target(Unhandled a, Unhandled3 b) => Unhandled.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled3))]
    public void MatchInstanceMethod(Unhandled a, Unhandled3 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
