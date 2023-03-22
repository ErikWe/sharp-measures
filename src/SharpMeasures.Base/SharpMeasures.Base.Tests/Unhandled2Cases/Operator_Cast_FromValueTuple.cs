namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Operator_Cast_FromValueTuple
{
    private static Unhandled2 Target((Unhandled, Unhandled) components) => (Unhandled2)components;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchFromValueTuple(Unhandled x, Unhandled y)
    {
        var expected = Unhandled2.FromValueTuple((x, y));

        var actual = Target((x, y));

        Assert.Equal(expected, actual);
    }
}
