namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Multiply_Unhandled_Unhandled4
{
    private static Unhandled4 Target(Unhandled a, Unhandled4 b) => Unhandled.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled4))]
    public void MatchInstanceMethod(Unhandled a, Unhandled4 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
