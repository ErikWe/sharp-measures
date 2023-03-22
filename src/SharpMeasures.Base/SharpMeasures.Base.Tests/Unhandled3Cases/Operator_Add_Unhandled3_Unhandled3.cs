namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Operator_Add_Unhandled3_Unhandled3
{
    private static Unhandled3 Target(Unhandled3 a, Unhandled3 b) => a + b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void MatchMethod(Unhandled3 a, Unhandled3 b)
    {
        var expected = Unhandled3.Add(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
