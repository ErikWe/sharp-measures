namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Operator_Cast_ToValueTuple
{
    private static (Unhandled, Unhandled, Unhandled) Target(Unhandled3 vector) => ((Unhandled, Unhandled, Unhandled))vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchToValueTuple(Unhandled3 vector)
    {
        var expected = vector.ToValueTuple();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
