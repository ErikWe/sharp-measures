namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Operator_Add_Unhandled4_Unhandled4
{
    private static Unhandled4 Target(Unhandled4 a, Unhandled4 b) => a + b;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled4))]
    public void MatchMethod(Unhandled4 a, Unhandled4 b)
    {
        var expected = Unhandled4.Add(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
