namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Operator_Cast_FromValueTuple
{
    private static Unhandled3 Target((Unhandled, Unhandled, Unhandled) components) => (Unhandled3)components;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchFromValueTuple(Unhandled x, Unhandled y, Unhandled z)
    {
        var expected = Unhandled3.FromValueTuple((x, y, z));

        var actual = Target((x, y, z));

        Assert.Equal(expected, actual);
    }
}
