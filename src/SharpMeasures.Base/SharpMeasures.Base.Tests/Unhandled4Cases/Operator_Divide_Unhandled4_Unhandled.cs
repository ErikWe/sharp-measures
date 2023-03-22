namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Operator_Divide_Unhandled4_Unhandled
{
    private static Unhandled4 Target(Unhandled4 a, Unhandled b) => a / b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled))]
    public void MatchMethod(Unhandled4 a, Unhandled b)
    {
        var expected = Unhandled4.Divide(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
