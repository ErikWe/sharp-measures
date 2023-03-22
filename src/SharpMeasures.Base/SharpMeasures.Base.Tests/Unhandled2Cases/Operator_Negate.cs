namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Operator_Negate
{
    private static Unhandled2 Target(Unhandled2 vector) => -vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchMethod(Unhandled2 vector)
    {
        var expected = vector.Negate();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
