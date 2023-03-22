namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Plus
{
    private static Unhandled Target(Unhandled scalar) => +scalar;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMethod(Unhandled unhandled)
    {
        var expected = unhandled.Plus();

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
