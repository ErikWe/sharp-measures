namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Operator_Cast_FromValueTuple
{
    private static Unhandled4 Target((Unhandled, Unhandled, Unhandled, Unhandled) components) => (Unhandled4)components;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchFromValueTuple(Unhandled x, Unhandled y, Unhandled z, Unhandled w)
    {
        var expected = Unhandled4.FromValueTuple((x, y, z, w));

        var actual = Target((x, y, z, w));

        Assert.Equal(expected, actual);
    }
}
