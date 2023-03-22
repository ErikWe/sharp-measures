namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Multiply_Unhandled3
{
    private static Unhandled3 Target(Unhandled unhandled, Unhandled3 factor) => unhandled.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled3))]
    public void MatchMultiply3(Unhandled unhandled, Unhandled3 factor)
    {
        var expected = unhandled.Multiply3(factor);

        var actual = Target(unhandled, factor);

        Assert.Equal(expected, actual);
    }
}
