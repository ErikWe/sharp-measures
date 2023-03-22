namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Operator_Plus
{
    private static Unhandled2 Target(Unhandled2 vector) => +vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchMethod(Unhandled2 vector)
    {
        var expected = vector.Plus();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
