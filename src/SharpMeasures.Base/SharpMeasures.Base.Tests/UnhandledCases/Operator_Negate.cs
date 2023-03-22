namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Negate
{
    private static Unhandled Target(Unhandled unhandled) => -unhandled;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMethod(Unhandled unhandled)
    {
        var expected = unhandled.Negate();

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
