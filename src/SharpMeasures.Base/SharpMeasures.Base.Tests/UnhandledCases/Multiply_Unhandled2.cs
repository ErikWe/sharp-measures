namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Multiply_Unhandled2
{
    private static Unhandled2 Target(Unhandled unhandled, Unhandled2 factor) => unhandled.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled2))]
    public void MatchMultiply2(Unhandled unhandled, Unhandled2 factor)
    {
        var expected = unhandled.Multiply2(factor);

        var actual = Target(unhandled, factor);

        Assert.Equal(expected, actual);
    }
}
