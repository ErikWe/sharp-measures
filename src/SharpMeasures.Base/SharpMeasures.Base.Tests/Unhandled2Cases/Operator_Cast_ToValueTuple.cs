namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Operator_Cast_ToValueTuple
{
    private static (Unhandled, Unhandled) Target(Unhandled2 vector) => ((Unhandled, Unhandled))vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchToValueTuple(Unhandled2 vector)
    {
        var expected = vector.ToValueTuple();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
