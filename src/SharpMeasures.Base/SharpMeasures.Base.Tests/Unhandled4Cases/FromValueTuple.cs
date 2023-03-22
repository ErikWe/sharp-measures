namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class FromValueTuple
{
    private static Unhandled4 Target((Unhandled, Unhandled, Unhandled, Unhandled) components) => Unhandled4.FromValueTuple(components);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchConstructor(Unhandled x, Unhandled y, Unhandled z, Unhandled w)
    {
        Unhandled4 expected = new(x, y, z, w);

        var actual = Target((x, y, z, w));

        Assert.Equal(expected, actual);
    }
}
