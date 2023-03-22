namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Operator_Plus
{
    private static Unhandled4 Target(Unhandled4 vector) => +vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchMethod(Unhandled4 vector)
    {
        var expected = vector.Plus();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
