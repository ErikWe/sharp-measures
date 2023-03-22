namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class FromValueTuple
{
    private static Unhandled2 Target((Unhandled, Unhandled) components) => Unhandled2.FromValueTuple(components);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchConstructor(Unhandled x, Unhandled y)
    {
        Unhandled2 expected = new(x, y);

        var actual = Target((x, y));

        Assert.Equal(expected, actual);
    }
}
