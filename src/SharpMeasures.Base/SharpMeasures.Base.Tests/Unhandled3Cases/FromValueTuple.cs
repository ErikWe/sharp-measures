namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class FromValueTuple
{
    private static Unhandled3 Target((Unhandled, Unhandled, Unhandled) components) => Unhandled3.FromValueTuple(components);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchConstructor(Unhandled x, Unhandled y, Unhandled z)
    {
        Unhandled3 expected = new(x, y, z);

        var actual = Target((x, y, z));

        Assert.Equal(expected, actual);
    }
}
