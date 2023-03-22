namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Operator_Add_Unhandled2_Unhandled2
{
    private static Unhandled2 Target(Unhandled2 a, Unhandled2 b) => a + b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void MatchMethod(Unhandled2 a, Unhandled2 b)
    {
        var expected = Unhandled2.Add(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
